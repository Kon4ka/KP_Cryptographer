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
    public class SoloveyShtrassenTest : IPrimeTest
    {
        public bool SimplifyCheck(BigInteger value, double probability)
        {
            int T = 10;
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            if (value == 2 || value == 3)   //Проверка мелких чисел
                return true;
            if (value < 2 || value % 2 == 0)
                return false;

            for (int i = 0; i < T; i++)
            {
                BigInteger a = SimplePrime.RandomInRange(rnd, 2, value - 1);    //Выбираем большое случайное а по границам [2 до n-1]
                if (BigInteger.GreatestCommonDivisor(a, value) > 1) //Если НОД(a, n) > 1, тогда число составное
                    return false;
                BigInteger y = BigInteger.ModPow(a, (value - 1) / 2, value);    // Считаем х = а^((n-1)/2) mod n
                BigInteger x = LegandrYakobiService.GetYakobiSymbol(a, value);  // Получаем символ Якоби
                if (x < 0)      
                    x += value;
                if (y != x % value)     // Если у не остаток от деоения х на n то число составное.
                    return false;
            }

            return true;    // Если дошли, то число, вероятно простое
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
