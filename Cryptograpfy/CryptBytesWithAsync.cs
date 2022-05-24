using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KP_Crypt.Cryptograpfy.CryptModes;

namespace KP_Crypt.Cryptograpfy
{
    public static class CryptByteWithArrayAsync
	{
		public static async Task<byte[]> CryptByteArrayAsync(byte[] infoToCrypt, ECB coder)
		{
			if (coder is null)
			{
				return null;
			}

			int sizeCryptPie = coder.GetCryptPartSize();


			byte addBytesCount = Convert.ToByte(sizeCryptPie - (byte)(infoToCrypt.Length % sizeCryptPie));


			if (addBytesCount == sizeCryptPie)
			{
				addBytesCount = 0;
			}

			byte[] inputInfo = new byte[infoToCrypt.Length + addBytesCount];
			byte[] outputInfo = new byte[infoToCrypt.Length + addBytesCount + 2];
			infoToCrypt.CopyTo(inputInfo, 0);


			byte[] outputInfo_ = await coder.Encrypt(inputInfo, 6);	//-await

			Array.Copy(outputInfo_, outputInfo, outputInfo_.Length);

			outputInfo[outputInfo.Length - 1] = Convert.ToByte(addBytesCount);
			outputInfo[outputInfo.Length - 2] = 0;

			return outputInfo;
		}

		public static async Task<byte[]> UnCryptByteArrayAsync(byte[] infoToDecrypt, ECB coder)
		{
			if (coder is null)
			{
				return null;
			}

			int sizeCryptPie = coder.GetCryptPartSize();

			byte addBytesCount = infoToDecrypt[infoToDecrypt.Length - 1];

			byte[] inputInfo = new byte[infoToDecrypt.Length - 2];
			byte[] outputInfoWithoutAddBytes = new byte[infoToDecrypt.Length - addBytesCount - 2];

			Array.Copy(infoToDecrypt, inputInfo, inputInfo.Length);

			byte[] outputInfo = await coder.Decrypt(inputInfo, 6);

			Array.Copy(outputInfo, outputInfoWithoutAddBytes, outputInfoWithoutAddBytes.Length);

			return outputInfoWithoutAddBytes;
		}
	}
}
