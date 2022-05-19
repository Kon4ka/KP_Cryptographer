﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy.SimpleTests
{
    public interface IPrimeTest
    {
        bool SimplifyCheck(BigInteger value, double probability);

        int GetCountRounds(double probability);


    }
}