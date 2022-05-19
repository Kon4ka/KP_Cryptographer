using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy	//Todo
{
	public static class CryptByteArrayWith
    {
		public static byte[] CryptByteArray(byte[] infoToCrypt, CoderBase coder)
		{
			if (coder is null)	//если неизвестно, чем расшифровывать
			{
				return null;
			}

			int cryptPartSize = coder.GetCryptPartSize();	//Размер блока

			// Размер дополнения до фулл байта
			byte addBytesCount = Convert.ToByte(cryptPartSize - (byte)(infoToCrypt.Length % cryptPartSize));

			// Если не надо добавлять
			if (addBytesCount == cryptPartSize)
			{
				addBytesCount = 0;
			}

			byte[] InputInfo = new byte[infoToCrypt.Length + addBytesCount];		// Исходный массив
			byte[] OutputInfo = new byte[infoToCrypt.Length + addBytesCount];	// После паддинга
			infoToCrypt.CopyTo(InputInfo, 0);

			for (int i = 0; i < addBytesCount; i++)
            {
				InputInfo[InputInfo.Length - i - 1] = Convert.ToByte(addBytesCount);    // Паддинг PKCS7 (1) - del
			}

			for (long i = 0; i < InputInfo.Length; i += cryptPartSize)	// Шагаем по блокам
			{
				byte[] currentPart = new byte[cryptPartSize];
				for (int j = 0; j < cryptPartSize; j++)
				{
					currentPart[j] = InputInfo[i + j];		//Заполняем текущий блок
				}

				byte[] cryptBytes = coder.Encrypt(currentPart);	// Шифруем блок

				for (int j = 0; j < cryptPartSize; j++)
				{
					OutputInfo[i + j] = cryptBytes[j];		// Копируем зашифрованное в выходной массив
				}
			}

/*			OutputInfo[OutputInfo.Length - 1] = Convert.ToByte(addBytesCount);
			OutputInfo[OutputInfo.Length - 2] = 0;*/

			return OutputInfo;
		}



		public static byte[] UnCryptByteArray(byte[] infoToDecrypt, CoderBase coder)
		{
			if (coder is null)
			{
				return null;
			}

			int cryptPartSize = coder.GetCryptPartSize();
			byte addBytesCount;

			byte[] paddingPart = new byte[cryptPartSize];

			for (int j = 0; j < cryptPartSize; j++)
			{
				paddingPart[j] = infoToDecrypt[infoToDecrypt.Length-cryptPartSize + j];	// Дешифрование блока с паддингом
			}
			byte[] paddingBytes = coder.Decrypt(paddingPart);

			if (paddingBytes[paddingBytes.Length-1] == paddingBytes[paddingBytes.Length - 2])	// Вычисление кол-ва пустот
            {
				addBytesCount = paddingBytes[paddingBytes.Length - 1];
			}
			else if (paddingBytes[paddingBytes.Length - 1] == 1)
            {
				addBytesCount = 1;
			} else
            {
				addBytesCount = 0;
			}

			byte[] OutputInfo = new byte[infoToDecrypt.Length - addBytesCount];

			// Запись последнего расшифрованного блока (чтоб не дешифровать его дважды)
			for (int j = 0; j < cryptPartSize && (infoToDecrypt.Length - cryptPartSize + j) < OutputInfo.Length; j++)
			{
				OutputInfo[infoToDecrypt.Length - cryptPartSize + j] = paddingBytes[j];
			}

			for (long i = 0; i < infoToDecrypt.Length - cryptPartSize; i += cryptPartSize) // Дешифровка всего кроме последнего
			{
				byte[] currentPart = new byte[cryptPartSize];
				for (int j = 0; j < cryptPartSize; j++)
				{
					currentPart[j] = infoToDecrypt[i + j];
				}

				byte[] cryptBytes = coder.Decrypt(currentPart);

				for (int j = 0; j < cryptPartSize && (i + j) < OutputInfo.Length; j++)
				{
					OutputInfo[i + j] = cryptBytes[j];
				}
			}

			return OutputInfo;
		}

	}
}

