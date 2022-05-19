using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KP_Crypt.Cryptograpfy.FROGalg;
using KP_Crypt.Cryptograpfy.EiGamalAlg;
using KP_Crypt.Cryptograpfy.CryptModes;

namespace KP_Crypt.Cryptograpfy
{
    public class CryptographyEngine
    {
        private static CryptographyEngine engine = null; //singltone 

        public FROG frog = null;
		public EiGamal eigamal = null;

        public string frogKeyString = "";
		public string frogIVectorString = "";

		private ECB frogECB = null;

		private CryptographyEngine()
        {
			eigamal = new EiGamal();
			eigamal.KeyGenerate();
        }

		public static CryptographyEngine GetCryptographyEngine()
        {
            if (engine == null)
            {
                engine = new CryptographyEngine();
            }
            return engine;
        }

		public void CreateFROG(byte[] frogKey, byte[] frogIV)
		{
			frog = new FROG(frogKey);

			frogECB = new ECB(frog);
			//frogCBC = new CBC(frog, frogIV); TODO
			//frogCFB = new CFB(frog, frogIV);
			//frogOFB = new OFB(frog, frogIV);

			frogKeyString += " => " + Encoding.Default.GetString(frogKey);
		}

		public byte[][] FROGKeyGeneration()
		{
			byte[][] result = new byte[2][];

			do
			{
				Random rand = new Random();
				byte[] frogKey = new byte[16];
				byte[] frogIV = new byte[16];
				rand.NextBytes(frogKey);
				rand.NextBytes(frogIV);

				frogKey[0] &= 1;
				frogKey[15] &= 1;

				frogIV[0] &= 1;
				frogIV[15] &= 1;

				frog = new FROG(frogKey);

				frogECB = new ECB(frog);
				//frogCBC = new CBC(frog, frogIV);
				//frogCFB = new CFB(frog, frogIV); TODO
				//frogOFB = new OFB(frog, frogIV);

				result[0] = frogKey;
				result[1] = frogIV;

				frogKeyString = Encoding.Default.GetString(frogKey) + " => ";
				frogIVectorString = Encoding.Default.GetString(frogIV);
			}
			while (frogKeyString.Contains("\n") || frogKeyString.Contains("\r") || frogKeyString.Contains("\0"));

			return result;
		}

		public byte[] CryptWithEiGamal(byte[] inputBytes)
		{
			return eigamal.Encrypt(inputBytes);
		}
		public byte[] UnCryptWithEiGamal(byte[] inputBytes)
		{
			return eigamal.Decrypt(inputBytes);
		}

		public async Task<byte[]> CryptWithFROGAsync(byte[] inputBytes, CryptModesEn cryptmode)
		{
			switch (cryptmode)
			{
				case CryptModesEn.ECB: return await CryptBytesWithAsync.CryptBytesAsync(inputBytes, frogECB);
				default: return await CryptBytesWithAsync.CryptBytesAsync(inputBytes, frogECB);
			}
			//return CryptByteArrayWith.CryptByteArray(inputBytes, frog);
		}
		public async Task<byte[]> UnCryptWithFROGAsync(byte[] inputBytes, CryptModesEn cryptmode)
		{
			switch (cryptmode)
			{
				case CryptModesEn.ECB: return await CryptBytesWithAsync.UnCryptBytesAsync(inputBytes, frogECB);
				default: return await CryptBytesWithAsync.UnCryptBytesAsync(inputBytes, frogECB);
			}
		}

		public void SetEGKeys(long[] keys)
		{
			eigamal.publicKey[0] = keys[0];
			eigamal.publicKey[1] = keys[1];
			eigamal.publicKey[2] = keys[2];
		}
		public void SetEGKeysStr(string[] keys)
		{
			eigamal.publicKey[0] = Convert.ToInt64(keys[0]);
			eigamal.publicKey[1] = Convert.ToInt64(keys[1]);
			eigamal.publicKey[2] = Convert.ToInt64(keys[2]);
		}
		public ulong[] GetEGKeys()
		{
			return new ulong[] { (ulong)eigamal.myPublicKey[0], (ulong)eigamal.myPublicKey[1], (ulong)eigamal.myPublicKey[2] };
		}
	}
}
