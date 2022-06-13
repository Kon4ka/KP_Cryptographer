using System;
using System.Collections;
using System.Collections.Generic;

namespace KP_Crypt.Cryptograpfy.FROGalg
{
    public class FROG : CoderBase
    {
        private byte[][][] _encryptRoundKeys = null;
        private byte[][][] _decryptRoundKeys = null;

        public static int[] _keySizeRange = new int[] { 5, 125 };
        public static int _sizeToExpand = 2304;
        private static byte[] _masterKey = new byte[]//
        {
            113,  21, 232,  18, 113,  92,  63, 157, 124, 193, 166, 197, 126,  56, 229, 229,
            156, 162,  54,  17, 230,  89, 189,  87, 169,   0,  81, 204,   8,  70, 203, 225,
            160,  59, 167, 189, 100, 157,  84,  11,   7, 130,  29,  51,  32,  45, 135, 237,
            139,  33,  17, 221,  24,  50,  89,  74,  21, 205, 191, 242,  84,  53,   3, 230,
            231, 118,  15,  15, 107,   4,  21,  34,   3, 156,  57,  66,  93, 255, 191,   3,
             85, 135, 205, 200, 185, 204,  52,  37,  35,  24,  68, 185, 201,  10, 224, 234,
              7, 120, 201, 115, 216, 103,  57, 255,  93, 110,  42, 249,  68,  14,  29,  55,
            128,  84,  37, 152, 221, 137,  39,  11, 252,  50, 144,  35, 178, 190,  43, 162,
            103, 249, 109,   8, 235,  33, 158, 111, 252, 205, 169,  54,  10,  20, 221, 201,
            178, 224,  89, 184, 182,  65, 201,  10,  60,   6, 191, 174,  79,  98,  26, 160,
            252,  51,  63,  79,   6, 102, 123, 173,  49,   3, 110, 233,  90, 158, 228, 210,
            209, 237,  30,  95,  28, 179, 204, 220,  72, 163,  77, 166, 192,  98, 165,  25,
            145, 162,  91, 212,  41, 230, 110,   6, 107, 187, 127,  38,  82,  98,  30,  67,
            225,  80, 208, 134,  60, 250, 153,  87, 148,  60,  66, 165,  72,  29, 165,  82,
            211, 207,   0, 177, 206,  13,   6,  14,  92, 248,  60, 201, 132,  95,  35, 215,
            118, 177, 121, 180,  27,  83, 131,  26,  39,  46,  12
        };

        public FROG(byte[] key) : base()
        {
            if (key.Length < _keySizeRange[0] || key.Length > _keySizeRange[1])
                throw new ArgumentException("Invalid key lenght");

            _chipherPartSize = 16;

            _encryptRoundKeys = RoundKeyGenerate(key, Way.Encrypt);
            _decryptRoundKeys = RoundKeyGenerate(key, Way.Decrypt);
        }

        public override byte[] Decrypt(byte[] infoBytes)
        {
        
           byte[] outInfo = new byte[infoBytes.Length];
           infoBytes.CopyTo(outInfo, 0);

           for (int round = 8; round > 0; round--)
           {
               for (int i = _chipherPartSize - 1; i >= 0; i--)
               {
                   // -4. По индексу из ключа (его 3ей части)
                   // XOR'им определенные байты данных друг с другом
                   byte index = _decryptRoundKeys[round-1][2][i];
                   outInfo[index] ^= outInfo[i];

                   // -3. Если предпоследний байт то XOR'им с ним последний
                   if (i < _chipherPartSize - 1)
                       outInfo[i + 1] ^= outInfo[i];
                   // -2. Выбирается байт, порядковый номер которого равен значению, вычисленному на первом шаге
                   outInfo[i] = _decryptRoundKeys[round-1][1][outInfo[i]];
                   // -1. XOR байта блока и байта раундового ключа
                   outInfo[i] ^= _decryptRoundKeys[round-1][0][i];
               }
           }
           return outInfo;
        }

        public override byte[] Encrypt(byte[] infoBytes)
        {
            byte[] outInfo = new byte[infoBytes.Length];
            infoBytes.CopyTo(outInfo, 0);

            //Проходим 8 раундов (в каждом работаем с одним блоком)
            for (int round = 0; round < 8; round++)
            {
                // Работаем в рамках одного блока
                for (int i = 0; i < _chipherPartSize; i++)
                {
                    // 1. XOR байта блока и байта раундового ключа
                    outInfo[i] ^= _encryptRoundKeys[round][0][i];
                    // 2. Выбирается байт, порядковый номер которого равен значению, вычисленному на первом шаге
                    outInfo[i] = _encryptRoundKeys[round][1][outInfo[i]];
                    // 3. Если предпоследний байт то XOR'им с ним последний
                    if (i < _chipherPartSize - 1)
                        outInfo[i + 1] ^= outInfo[i];
                    // 4. По индексу из ключа (его 3ей части)
                    // XOR'им определенные байты данных друг с другом
                    byte index = _encryptRoundKeys[round][2][i];
                    outInfo[index] ^= outInfo[i];
                }
            }
            return outInfo;
        }

        private byte[][][] RoundKeyGenerate(byte[] key, Way direction)
        {
            // Клонируем ключ шифрования до длинны 2304 (1)
            byte[] keyExpanded = Expanding(key);
            // Клонируем также мастер ключ дл длинны 2304 (2)
            byte[] masterKeyExpanded = Expanding(_masterKey);
            // XOR'им биты ключа и мастер ключа (3)
            for (int i = 0; i < _sizeToExpand; i++)
                keyExpanded[i] = (byte)(keyExpanded[i] ^ masterKeyExpanded[i]);
            // Форматируем ключ (4)
            byte[][][] preliminaryKey = FormatRoundKey(keyExpanded, Way.Encrypt);
            // Шифруем CBC массив нулей этим ключом и IV (5)
            byte[] IV = new byte[_chipherPartSize];
            Array.Copy(keyExpanded, IV, _chipherPartSize); //Первые 16 бит исходного ключа XOR 16
            IV[0] ^= (byte)key.Length;

            byte[] result = EncryptZeros(preliminaryKey, IV);
            // Форматируем ключ (6)
            return FormatRoundKey(result, direction);
        }

        private T[] Expanding<T>(T[] array)     //Дублирование до достижения длинны 2304
        {
            T[] result = new T[_sizeToExpand];
            for (int i = 0; i < _sizeToExpand; i++)
                result[i] = array[i % array.Length];
            return result;
        }

        private byte[] EncryptZeros(byte[][][] preliminaryKey, byte[] iv)
        {
            //Вычисляем кол-во блоков
            int blocksCount = _sizeToExpand / _chipherPartSize;

            byte[] buf = new byte[_chipherPartSize];
            byte[] result = new byte[_sizeToExpand];

            for (int i = 0; i < blocksCount; i++)
            {
                EncryptCBC(buf, iv, preliminaryKey, 0, result, i * _chipherPartSize);
            }
            return result;
        }

        private byte[][][] FormatRoundKey(byte[] expandedKey, Way direction)
        {
            int bytesInKey = 288;// 16 + 256 таблица перестановок + 16 табл. индексов
            byte[][][] result = new byte[8][][];// result: №раунда, key(16, 256, 16), индекс байта
            for (int i = 0; i < 8; i++)
            {
                // Генерим из общего ключа 3 части (1)
                byte[] key1 = new byte[16];
                byte[] key2 = new byte[256];
                byte[] key3 = new byte[16];

                Array.Copy(expandedKey, i * bytesInKey, key1, 0, 16);
                Array.Copy(expandedKey, i * bytesInKey + 16, key2, 0, 256);
                Array.Copy(expandedKey, i * bytesInKey + 272, key3, 0, 16);

                //Первые 16 байт переходят в итоговый ключ без изменений
                // Обрабатываем таблицу перестановок (2)
                Format(key2);

                // Если генерим ключи для расшифровки то дополнительно инвертируем
                if (direction == Way.Decrypt)
                    key2 = Invert(key2);

                // Так же обрабатываем таблицу индексов (3)
                Format(key3);
                // Проводим анализ цепочек ссылок таблицы индексов
                // Должны получить одну цепочку ссылок
                MakeSingleCycle(key3); 

                // Меняем некоторые элементы 
                // key3[j] == j + 1 -> key3[j] = ((j + 2) % 16)
                for (int j = 0; j < 16; j++)
                    if (key3[j] == j + 1)
                        key3[j] = (byte)((j + 2) % 16);

                // Собираем итоговый элемент массива раундовых ключей
                result[i] = new byte[3][]
                {
                    key1, key2, key3
                };
            }
            return result;
        }

        private byte[] Invert(byte[] values)
        {
            byte[] result = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
                result[values[i]] = (byte)i;
            return result;
        }

        private void Format(byte[] table)
        {
            List<byte> U = new List<byte>(table.Length); //Лист с 0...n значениями
            for (int i = 0; i < table.Length; i++)
                U.Add((byte)i);

            int pastInd = 0;
            // Определяем байт таблицы перестановок по формуле
            // table[i] = U[(pastInd + table[i]) % table.Length]
            for (int i = 0; i < table.Length; i++)
            {
                int currInd = (pastInd + table[i]) % U.Count;
                pastInd = currInd;
                table[i] = U[currInd];
                U.RemoveAt(currInd);
            }
        }

        private void MakeSingleCycle(byte[] table)
        {
            BitArray inCycle = new BitArray(table.Length, false);

            int index = 0;
            while (true)
            {
                // Текущая позиция является частью цикла
                inCycle[index] = true;  
                if (inCycle[table[index]]) // Если следующая ссылка уже была в цикле
                {
                    // Ищем другой индекс для следующей ссылки
                    int nextIndex = FirstIndexOf(inCycle, false);
                    if (nextIndex != -1)
                    {
                        // Если другоей индекс есть -> перенаправляем
                        table[index] = (byte)nextIndex;
                    }
                    else
                    {
                        table[index] = 0; // Полный цикл достигнут
                        break;
                    }
                }
                //Двигаемся "По ссылке"
                index = table[index];
            }
        }
        public int FirstIndexOf(BitArray bitArray, bool value)  //Ищем первое вхождение false
        {
            for (int i = 0; i < bitArray.Length; i++)
                if (bitArray[i] == value)
                    return i;
            return -1;
        }

        private byte[] EncryptCBC(byte[] infoBytes, byte[] iv, byte[][][] encryptRoundKeys, int inputOffset, byte[] result, int resultOffset)
        {
            Array.Copy(infoBytes, inputOffset, result, resultOffset, _chipherPartSize);

            for (int i = 0; i < _chipherPartSize; i++)
                result[i] ^= iv[i];

            for (int round = 0; round < 8; round++)
            {
                for (int i = 0; i < _chipherPartSize; i++)
                {
                    // 1. XOR байта блока и байта раундового ключа (учитывая смещение)
                    result[resultOffset + i] ^= encryptRoundKeys[round][0][i];
                    // 2. Выбирается байт, порядковый номер которого равен значению, вычисленному на первом шаге
                    result[resultOffset + i] = encryptRoundKeys[round][1][result[resultOffset + i]];
                    // 3. Если предпоследний байт то XOR'им с ним последний
                    if (i < _chipherPartSize - 1)
                        result[resultOffset + i + 1] ^= result[resultOffset + i];
                    // 4. По индексу из ключа (его 3ей части)
                    // XOR'им определенные байты данных друг с другом
                    byte index = encryptRoundKeys[round][2][i];
                    result[resultOffset + index] ^= result[resultOffset + i];
                }
            }

            return result;
        }

        protected override void KeyGeneration() //Maybe Clear
        {

        }
    }


}
