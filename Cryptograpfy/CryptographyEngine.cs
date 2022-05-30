using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KP_Crypt.Cryptograpfy.FROGalg;
using KP_Crypt.Cryptograpfy.EiGamalAlg;
using KP_Crypt.Cryptograpfy.CryptModes;
using KP_Crypt.Cryptograpfy.CryptingModes;

namespace KP_Crypt.Cryptograpfy
{
    public class CryptographyEngine
    {
        private static CryptographyEngine engine = null; //singltone 
        public FROG frog = null;
		public ElGamal elgamal = null;

		public delegate void PrograssInBarCE(long i, int len, int step);

		public static event PrograssInBarCE upper;

		public string frogKeyString = "";
		public string frogIVectorString = "";

		private ECB frogECB = null;
		private CBC frogCBC = null;
		private CFB frogCFB = null;
		private OFB frogOFB = null;
		private CTR frogCTR = null;

		private CryptographyEngine()
        {
			CryptByteArrayWith.onCount += PrograssInBarCBW;
			elgamal = new ElGamal();
			elgamal.KeyGenerate();
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
			try
			{
				frog = new FROG(frogKey);
			}
			catch
            {
				///
            }
			frogECB = new ECB(frog);
			frogCBC = new CBC(frog, frogIV); 
			frogCFB = new CFB(frog, frogIV);
			frogOFB = new OFB(frog, frogIV);
			frogCTR = new CTR(frog, frogIV);

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
				frogCBC = new CBC(frog, frogIV);
				frogCFB = new CFB(frog, frogIV);
				frogOFB = new OFB(frog, frogIV);
				frogCTR = new CTR(frog, frogIV);

				result[0] = frogKey;
				result[1] = frogIV;

				frogKeyString = Encoding.Default.GetString(frogKey) + " => ";
				frogIVectorString = Encoding.Default.GetString(frogIV);
			}
			while (frogKeyString.Contains("\n") || frogKeyString.Contains("\r") || frogKeyString.Contains("\0") || frogIVectorString.Contains("\0"));

			return result;
		}

		public byte[] CryptWithEiGamal(byte[] inputBytes)
		{
			return elgamal.Encrypt(inputBytes);
		}
		public byte[] UnCryptWithEiGamal(byte[] inputBytes)
		{
			return elgamal.Decrypt(inputBytes);
		}

		public void PrograssInBarCBW(long i, int len, int step)
        {
			upper(i, len, step);
        }

		public async Task<byte[]> CryptWithFROGAsync(byte[] inputBytes, CryptModesEn cryptmode)
		{
			switch (cryptmode)
			{
				case CryptModesEn.ECB: return await CryptByteWithArrayAsync.CryptByteArrayAsync(inputBytes, frogECB);
				case CryptModesEn.CBC: return CryptByteArrayWith.CryptByteArray(inputBytes, frogCBC);
				case CryptModesEn.CFB: return CryptByteArrayWith.CryptByteArray(inputBytes, frogCFB);
				case CryptModesEn.OFB: return CryptByteArrayWith.CryptByteArray(inputBytes, frogOFB);
				case CryptModesEn.CTR: return CryptByteArrayWith.CryptByteArray(inputBytes, frogCTR);
				default: return CryptByteArrayWith.CryptByteArray(inputBytes, frogOFB);
			}
		}
		public async Task<byte[]> UnCryptWithFROGAsync(byte[] inputBytes, CryptModesEn cryptmode)
		{
			switch (cryptmode)
			{
				case CryptModesEn.ECB: return await CryptByteWithArrayAsync.UnCryptByteArrayAsync(inputBytes, frogECB);
				case CryptModesEn.CBC: return CryptByteArrayWith.UnCryptByteArray(inputBytes, frogCBC);
				case CryptModesEn.CFB: return CryptByteArrayWith.UnCryptByteArray(inputBytes, frogCFB);
				case CryptModesEn.OFB: return CryptByteArrayWith.UnCryptByteArray(inputBytes, frogOFB);
				case CryptModesEn.CTR: return CryptByteArrayWith.UnCryptByteArray(inputBytes, frogCTR);
				default: return CryptByteArrayWith.UnCryptByteArray(inputBytes, frogOFB);
			}
		}

		public void SetEGKeys(long[] keys)
		{

			elgamal.SetOtherPublicKey(keys[0], keys[1], keys[2]);
		}
		public void SetEGKeysStr(string[] keys)
		{
			elgamal.SetOtherPublicKey(Convert.ToInt64(keys[0]), Convert.ToInt64(keys[1]), Convert.ToInt64(keys[2]));
		}
		public ulong[] GetEGKeys()
		{
			return elgamal.GetMyPublicKey() ;
		}
	}
}
