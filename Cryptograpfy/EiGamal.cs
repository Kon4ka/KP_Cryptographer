using System;
using System.Text;
using System.Numerics;
using KP_Crypt.Cryptograpfy.Prime;
using System.Security.Cryptography;

namespace KP_Crypt.Cryptograpfy.EiGamalAlg
{
    public class EiGamal : CoderBase
    {
        public BigInteger[] myPublicKey = new BigInteger[3];  //p, g, y
        private BigInteger _privateKey;
        public BigInteger[] publicKey = new BigInteger[3];

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
                        BigInteger a = BigInteger.Parse(strA[i]);
                        BigInteger b = BigInteger.Parse(strA[i + 1]);
                        if ((a != 0) && (b != 0))
                        {
                            //BigInteger deM = mul(bi, power(ai, p - 1 - x, p), p);// m=b*(a^x)^(-1)mod p =b*a^(p-1-x)mod p - трудно было  найти нормальную формулу, в ней вся загвоздка
                            BigInteger deM = MultMOD(b, FastPow(a, myPublicKey[0] - 1 - _privateKey, myPublicKey[0]), myPublicKey[0]);
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
                        BigInteger k = Rand() % (publicKey[0] - 2) + 1; // 1 < k < (p-1)
                        BigInteger a = FastPow(publicKey[1], k, publicKey[0]);
                        BigInteger b = MultMOD(FastPow(publicKey[2], k, publicKey[0]), m, publicKey[0]);
                        res += a + " " + b + " ";

                    }
                }
                return res;
            }
            return "";
        }

        public byte[] EncryptWithOthersKey(byte[] infoBytes, BigInteger[] key)
        {
            publicKey[0] = key[0];
            publicKey[1] = key[1];
            publicKey[2] = key[2];
            string infoStr = Encoding.Default.GetString(infoBytes);

            return Encoding.Default.GetBytes(Encrypting(infoStr));
        }

        public override byte[] Encrypt(byte[] infoBytes)
        {
            if (publicKey[0] == 0)
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

        public void KeyGenerate()
        {
            SimplePrime primes = new SimplePrime(PrimeTestMode.SoloveyShtrasen, 0.98, 64);
            BigInteger curKey;
            //Random r = new Random();
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            // P
            curKey = primes.GeneratePrimeDigit();
            myPublicKey[0] = curKey;

            //G
            myPublicKey[1] = SimplePrime.RandomInRange(rnd, 1, myPublicKey[0] - 1);
            //X
            _privateKey = SimplePrime.RandomInRange(rnd, 1, myPublicKey[0] - 2);
            //Y
            myPublicKey[2] = FastPow(myPublicKey[1], _privateKey, myPublicKey[0]);
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

        BigInteger Mult(BigInteger a, BigInteger b, BigInteger n) // a*b mod n - умножение a на b по модулю n
        {
            BigInteger sum = 0;
            for (BigInteger i = 0; i < b; i++)
            {
                sum += a;
                if (sum >= n)
                {
                    sum -= n;
                }
            }
            return sum;
        }
        private BigInteger Rand()//Ф-я получения случайного числа
        {
            Random random = new Random();
            return random.Next();
        }
    }
}
