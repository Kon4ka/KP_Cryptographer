using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KP_Crypt.Cryptograpfy.Prime;

namespace KP_Crypt.Cryptograpfy.SimpleTests
{
    public class FermaTest: IPrimeTest
    {
        public bool SimplifyCheck(BigInteger number, double probability)
        {
            int T = 12;
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            if (number == 2 || number == 3)   //Проверки мелких чисел
                return true;
            if (number < 2 || number % 2 == 0)
                return false;

            for (int i = 0; i < T; i++) //Проверяем кол-во раз Т
            {
                // Выбираем случайное больше число в диапазоне
                BigInteger a = SimplePrime.RandomInRange(rnd, 2, number - 2);   

                // Проверяем есть ли НОД у нашего числа и случайного больше 1, если есть - не простое
                if (BigInteger.GreatestCommonDivisor(a, number) != 1)
                    return false;
                // Если a^(n-1)(mod n) не 1 то тоже не простое
                if (BigInteger.ModPow(a, number - 1, number) != 1)  
                    return false;
            }
            return true;    // Если дошли - значит простое
        }

        public int GetCountRounds(double probability)
        {
            int count = 0;
            double extra = 1;
            while (1 - extra <= probability)
            {
                count++;
                extra *= 0.5;
            }
            return count;
        }
    }
}
