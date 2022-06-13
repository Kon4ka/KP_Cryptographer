using System;
using System.Text;
using System.Numerics;
using KP_Crypt.Cryptograpfy.Prime;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace KP_Crypt.Cryptograpfy.EiGamalAlg
{
    public class ElGamal 
    {
        private BigInteger[] _myPublicKey = new BigInteger[3];  //p, g, y
        private BigInteger _privateKey;
        private BigInteger[] _publicKey = new BigInteger[3];
        private Random random = new Random();

        public byte[] Decrypt(BigInteger[][] infoBytes)
        {
            List<byte> output = new List<byte>();

                if (infoBytes.Length > 0)
                {
                    for (long i = 0; i < infoBytes[0].Length; i++)
                    {
                        BigInteger ai = 0;
                        BigInteger bi = 0;
                        BigInteger a = infoBytes[0][i];
                        BigInteger b = infoBytes[1][i];
                        if ((a != 0) && (b != 0))
                        {
                            //BigInteger deM = mul(bi, power(ai, p - 1 - x, p), p);// m=b*(a^x)^(-1)mod p =b*a^(p-1-x)mod p - трудно было  найти нормальную формулу, в ней вся загвоздка
                            BigInteger deM = MultMOD(b, FastPow(a, _myPublicKey[0] - 1 - _privateKey, _myPublicKey[0]), _myPublicKey[0]);
                            //char m = (char)deM;
                            output.Add(deM.ToByteArray()[0]);
                        }
                    }
                    return output.ToArray();
                }
            return null;
        }

        private BigInteger[][] Encrypting(byte[] infoStr)       //BigInteger[][]
        {
            BigInteger[][] res = new BigInteger[2][];
            res[0] = new BigInteger[infoStr.Length];
            res[1] = new BigInteger[infoStr.Length];

            if (infoStr.Length > 0)
            {
                byte[] temp = new byte[infoStr.Length - 1];
                temp = infoStr; 
                for (long i = 0; i <= infoStr.Length - 1; i++)
                {
                    BigInteger m = new BigInteger(temp[i]);
                    if (m > 0) 
                    {
                        BigInteger k = Rand() % (_publicKey[0] - 2) + 1; // 1 < k < (p-1)
                        BigInteger a = FastPow(_publicKey[1], k, _publicKey[0]);
                        BigInteger b = MultMOD(FastPow(_publicKey[2], k, _publicKey[0]), m, _publicKey[0]);
                        res[0][i] = a;
                        res[1][i] = b;

                    }
                }
                return res;
            }
            return null;
        }

/*        struct ABBuffer
        {
            ulong aBlen;
            ulong origMessLen;
            ulong messLen;
            byte[] info;


            public byte[] BigIntsToByteArray(BigInteger[][] input)
            {
                byte[] res = new byte[8*3 + aBlen + origMessLen + messLen];
                BitConverter.GetBytes(aBlen).CopyTo(res, 0);
                BitConverter.GetBytes(origMessLen).CopyTo(res, 8);
                BitConverter.GetBytes(messLen).CopyTo(res, 16);
                for (int i = 0; i < input.Length; i++)  //a or b 
                    for (int j = 0; j < input.Length; j++)
                    {
                        res[8*3+i+j]
                    }
                        //info.CopyTo(res, 8 * 3);
                        return res;
            }
            public BigInteger[][] AllToBigIntArray(byte[] input)
            {

            }
        }*/

        public BigInteger[][] EncryptWithOthersKey(byte[] infoBytes, BigInteger[] key)
        {
            _publicKey[0] = key[0];
            _publicKey[1] = key[1];
            _publicKey[2] = key[2];

            return Encrypting(infoBytes);
        }

        public BigInteger[][] Encrypt(byte[] infoBytes)
        {
            if (_publicKey[0] == 0)
            {
                return null;
            }
            string infoStr = Encoding.Default.GetString(infoBytes);

            return Encrypting(infoBytes);
        }

        public void KeyGenerate()//collect
        {
            SimplePrime primes = new SimplePrime(PrimeTestMode.SoloveyShtrasen, 0.98, 64);//upper
            BigInteger curKey;
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            // P
            curKey = primes.GeneratePrimeDigit();
            _myPublicKey[0] = curKey;

            //G
            _myPublicKey[1] = SimplePrime.RandomInRange(rnd, 1, _myPublicKey[0] - 1);
            //X
            _privateKey = SimplePrime.RandomInRange(rnd, 1, _myPublicKey[0] - 2);
            //Y
            _myPublicKey[2] = FastPow(_myPublicKey[1], _privateKey, _myPublicKey[0]);
        }

        BigInteger MultMOD(BigInteger a, BigInteger b, BigInteger mod)
        {
            BigInteger res = 0; // Initialize result

            // Update a if it is more than
            // or equal to mod
            a %= mod;

            while (b > 0)
            {
                // If b is odd, add a with result
                if ((b & 1) > 0)
                    res = (res + a) % mod;

                // Here we assume that doing 2*a
                // doesn't cause overflow
                a = (2 * a) % mod;

                b >>= 1; // b = b / 2
            }

            return res;
        }

        BigInteger FastPow(BigInteger num, BigInteger pow, BigInteger mod) // a^b mod n - то же что ниже но быстрее
        {
            {
                BigInteger res = 1;
                while (pow > 0)
                {
                    if (pow % 2 == 1) res = (res * num) % mod;
                    num = (num * num) % mod;
                    pow >>= 1;
                }

                return res;
            }
        }

        public void SetMyPublicKey(BigInteger p, BigInteger g, BigInteger y)
        {
            _myPublicKey[0] = p;
            _myPublicKey[1] = g;
            _myPublicKey[2] = y;
        }

        public void SetOtherPublicKey(BigInteger p, BigInteger g, BigInteger y)
        {
            _publicKey[0] = p;
            _publicKey[1] = g;
            _publicKey[2] = y;
        }

        public ulong[] GetMyPublicKey()
        {
            ulong[] tmp = new ulong[3];

            tmp[0] = (ulong)_myPublicKey[0];
            tmp[1] = (ulong)_myPublicKey[1];
            tmp[2] = (ulong)_myPublicKey[2];
            return tmp;
        }
        public BigInteger[] GetOtherPublicKey()
        {
            return _publicKey;
        }
        private BigInteger Rand()//Ф-я получения случайного числа
        {
            return random.Next();
        }
    }
}
