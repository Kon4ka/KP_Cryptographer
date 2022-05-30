using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using KP_Crypt.Cryptograpfy.SimpleTests;
using System.Security.Cryptography;

namespace KP_Crypt.Cryptograpfy.Prime
{ 
    public enum PrimeTestMode
    {
        Ferm,
        MillerRabin,
        SoloveyShtrasen

    };
    public class SimplePrime
    {

        private PrimeTestMode _mode;    // Тест для проверки простоты
        private double _probability;    // Должная вероятность того, что число простое
        private int _length;            // Длинна числа в битах
        private Random _random;


        public SimplePrime(PrimeTestMode mode, double probability, int BitLength)
        {
            _mode = mode;
            _probability = probability;
            _length = BitLength;
            _random = new Random();
        }

        public BigInteger GeneratePrimeDigit()
        {
            BigInteger digit = GetRandCount(_length);

            switch (_mode)
            {
                case PrimeTestMode.Ferm:
                    {
                        FermaTest test = new FermaTest();
                        while (!test.SimplifyCheck(digit, _probability))
                        {
                            digit = GetRandCount(_length);
                        }
                    }
                    break;
                case PrimeTestMode.MillerRabin:
                    {
                        MillerRabinTest test = new MillerRabinTest();

                        while (!test.SimplifyCheck(digit, _probability))
                        {
                            digit = GetRandCount(_length);
                        }
                    }
                    break;
                case PrimeTestMode.SoloveyShtrasen:
                    {
                        SoloveyShtrassenTest test = new SoloveyShtrassenTest();
                        while (!test.SimplifyCheck(digit, _probability))
                        {
                            digit = GetRandCount(_length);
                        }
                    }
                    break;
                default:
                    break;
            }
            return digit;
        }

        public BigInteger GetRandCount(int bits)
        {
            byte[] count = new byte[bits / 8]; //генерация числа с условием 0...1 - нечетное и неотрицательное
            _random.NextBytes(count);

            if (bits > 8)
            {
                count[0] = (byte)((int)count[0] | 1); 

                count[count.Length - 1] = (byte)((int)count[count.Length - 1] & 0);
            }
            else
                count[0] = (byte)((int)count[0] & 0x7F);
            return (new BigInteger(count));

        }

        #region Help Functions 

        public static BigInteger RandomInRange(RandomNumberGenerator rng, BigInteger min, BigInteger max)
        {
            var bytes = new byte[10];

            new Random().NextBytes(bytes);
            new BigInteger(bytes);

            if (min > max)
            {
                var buff = min;
                min = max;
                max = buff;
            }

            // offset to set min = 0
            BigInteger offset = -min;
            min = 0;
            max += offset;

            var value = randomInRangeFromZeroToPositive(rng, max) - offset;
            return value;
        }

        private static BigInteger randomInRangeFromZeroToPositive(RandomNumberGenerator rng, BigInteger max)
        {
            BigInteger value;
            var bytes = max.ToByteArray();

            // count how many bits of the most significant byte are 0
            // NOTE: sign bit is always 0 because `max` must always be positive
            byte zeroBitsMask = 0b00000000;

            var mostSignificantByte = bytes[bytes.Length - 1];

            // we try to set to 0 as many bits as there are in the most significant byte, starting from the left (most significant bits first)
            // NOTE: `i` starts from 7 because the sign bit is always 0
            for (var i = 7; i >= 0; i--)
            {
                // we keep iterating until we find the most significant non-0 bit
                if ((mostSignificantByte & (0b1 << i)) != 0)
                {
                    var zeroBits = 7 - i;
                    zeroBitsMask = (byte)(0b11111111 >> zeroBits);
                    break;
                }
            }

            do
            {
                rng.GetBytes(bytes);

                // set most significant bits to 0 (because `value > max` if any of these bits is 1)
                bytes[bytes.Length - 1] &= zeroBitsMask;

                value = new BigInteger(bytes);

                // `value > max` 50% of the times, in which case the fastest way to keep the distribution uniform is to try again
            } while (value > max);

            return value;
        }

        private BigInteger RandWithBorder(BigInteger a, BigInteger b)
        {
            BigInteger res = BigInteger.Zero;
            Random r = new Random();
            while (res < a || res > b)
            {
                res = (BigInteger)(r.NextDouble() * (ulong.MaxValue));
            }
            return res > 0 ? res : -res;
        }

        #endregion
    }
}
