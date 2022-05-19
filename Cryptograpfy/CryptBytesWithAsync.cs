using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KP_Crypt.Cryptograpfy.CryptModes;

namespace KP_Crypt.Cryptograpfy
{
    public static class CryptBytesWithAsync
	{
		public static async Task<byte[]> CryptBytesAsync(byte[] inputBytes, ECB encoder)
		{
			if (encoder is null)
			{
				return null;
			}

			int sizeCryptPie = encoder.GetCryptPartSize();


			byte addBytesCount = Convert.ToByte(sizeCryptPie - (byte)(inputBytes.Length % sizeCryptPie));


			if (addBytesCount == sizeCryptPie)
			{
				addBytesCount = 0;
			}

			byte[] inputInfo = new byte[inputBytes.Length + addBytesCount];
			byte[] outputInfo = new byte[inputBytes.Length + addBytesCount];
			inputBytes.CopyTo(inputInfo, 0);


			byte[] outputInfo_ = await encoder.Encrypt(inputInfo, 6);

			Array.Copy(outputInfo_, outputInfo, outputInfo_.Length);

			outputInfo[outputInfo.Length - 1] = Convert.ToByte(addBytesCount);
			outputInfo[outputInfo.Length - 2] = 0;

			return outputInfo;
		}

		public static async Task<byte[]> UnCryptBytesAsync(byte[] inputBytes, ECB encoder)
		{
			if (encoder is null)
			{
				return null;
			}

			int sizeCryptPie = encoder.GetCryptPartSize();

			byte addBytesCount = inputBytes[inputBytes.Length - 1];

			byte[] inputInfo = new byte[inputBytes.Length - 2];
			byte[] outputInfoWithoutAddBytes = new byte[inputBytes.Length - addBytesCount - 2];

			Array.Copy(inputBytes, inputInfo, inputInfo.Length);

			byte[] outputInfo = await encoder.Decrypt(inputInfo, 6);

			Array.Copy(outputInfo, outputInfoWithoutAddBytes, outputInfoWithoutAddBytes.Length);

			return outputInfoWithoutAddBytes;
		}
	}
}
