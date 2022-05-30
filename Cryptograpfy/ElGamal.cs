using System;
using System.Text;
using System.Numerics;
using KP_Crypt.Cryptograpfy.Prime;
using System.Security.Cryptography;

namespace KP_Crypt.Cryptograpfy.EiGamalAlg
{
    public class ElGamal : CoderBase
    {
        private BigInteger[] _myPublicKey = new BigInteger[3];  //p, g, y
        private BigInteger _privateKey;
        private BigInteger[] _publicKey = new BigInteger[3];
        private Random random = new Random();

        public override byte[] Decrypt(byte[] infoBytes)
        {
            string infoStr = Encoding.Default.GetString(infoBytes);
            string output = "";
            if (infoStr.Length > 0)
            {
                string[] strA = infoStr.Split(' ');
                if (strA.Length > 0)
                {
                    for (long i = 0; i < strA.Length - 1; i += 2)
                    {
                        BigInteger ai = 0;
                        BigInteger bi = 0;
                        BigInteger a = BigInteger.Parse(strA[i]);//tryParse
                        BigInteger b = BigInteger.Parse(strA[i + 1]);//GetBytes
                        if ((a != 0) && (b != 0))
                        {
                            //BigInteger deM = mul(bi, power(ai, p - 1 - x, p), p);// m=b*(a^x)^(-1)mod p =b*a^(p-1-x)mod p - трудно было  найти нормальную формулу, в ней вся загвоздка
                            BigInteger deM = MultMOD(b, FastPow(a, _myPublicKey[0] - 1 - _privateKey, _myPublicKey[0]), _myPublicKey[0]);
                            //char m = (char)deM;
                            output += ((char)deM).ToString();
                        }
                    }
                    return Encoding.Default.GetBytes(output);
                }
            }
            return null;
        }

        private string Encrypting(string infoStr)
        {
            string res = "";

            if (infoStr.Length > 0)
            {
                char[] temp = new char[infoStr.Length - 1];
                temp = infoStr.ToCharArray();
                for (long i = 0; i <= infoStr.Length - 1; i++)
                {
                    BigInteger m = new BigInteger(temp[i]);
                    if (m > 0) 
                    {
                        BigInteger k = Rand() % (_publicKey[0] - 2) + 1; // 1 < k < (p-1)
                        BigInteger a = FastPow(_publicKey[1], k, _publicKey[0]);
                        BigInteger b = MultMOD(FastPow(_publicKey[2], k, _publicKey[0]), m, _publicKey[0]);
                        res += a + " " + b + " ";

                    }
                }
                return res;
            }
            return "";
        }

        public byte[] EncryptWithOthersKey(byte[] infoBytes, BigInteger[] key)
        {
            _publicKey[0] = key[0];
            _publicKey[1] = key[1];
            _publicKey[2] = key[2];
            string infoStr = Encoding.Default.GetString(infoBytes);

            return Encoding.Default.GetBytes(Encrypting(infoStr));
        }

        public override byte[] Encrypt(byte[] infoBytes)
        {
            if (_publicKey[0] == 0)
            {
                return null;
            }
            string infoStr = Encoding.Default.GetString(infoBytes);

            return Encoding.Default.GetBytes(Encrypting(infoStr));
        }

        protected override void KeyGeneration()
        {
            throw new ArgumentException();
        }

        public void KeyGenerate()//collect
        {
            SimplePrime primes = new SimplePrime(PrimeTestMode.SoloveyShtrasen, 0.98, 64);//upper
            BigInteger curKey;
            //Random r = new Random();
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
