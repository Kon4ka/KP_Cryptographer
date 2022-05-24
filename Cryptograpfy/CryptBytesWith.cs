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

			return OutputInfo;
		}



		public static byte[] UnCryptByteArray(byte[] infoToDecrypt, CoderBase coder)
		{
			if (coder is null)
			{
				return null;
			}

			int cryptPartSize = coder.GetCryptPartSize();
			byte addBytesCount = 0;

			byte[] outputInfoWithPadd = new byte[infoToDecrypt.Length - addBytesCount];
			int i;

			for (long m = 0; m < infoToDecrypt.Length - 1; m += cryptPartSize) // Дешифровка всего кроме последнего
			{
				byte[] currentPart = new byte[cryptPartSize];
				for (int j = 0; j < cryptPartSize; j++)
				{
					currentPart[j] = infoToDecrypt[m + j];
				}

				byte[] cryptBytes = coder.Decrypt(currentPart);

				for (int j = 0; j < cryptPartSize && (m + j) < outputInfoWithPadd.Length; j++)
				{
					outputInfoWithPadd[m + j] = cryptBytes[j];
				}
			}

			bool is_padding = false;
			for (i = outputInfoWithPadd.Length - 1; i >= 0; i--)
			{
				if (outputInfoWithPadd.Length - i == (byte)outputInfoWithPadd[i])
				{
					is_padding = true;
					for (int j = i; j < outputInfoWithPadd.Length - 1; j++)
					{
						if (outputInfoWithPadd[j] != outputInfoWithPadd[i])
							is_padding = false;
					}
					if (!is_padding) i = outputInfoWithPadd.Length;
					break;
				}
			}
			if (!is_padding) i = outputInfoWithPadd.Length;

			byte[] outdata = new byte[i];
			for (int j = 0; j < i; j++)
				outdata[j] = outputInfoWithPadd[j];
			return outdata;
		}

	}
}

