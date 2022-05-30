using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy
{
    public enum Way
    {
        Encrypt, Decrypt
    }
    public enum CryptModesEn
    {
        ECB, CBC, CFB, OFB, CTR, RD, RDH
    }
    public enum PrimeTestMode
    {
        Ferm,
        MillerRabin,
        SoloveyShtrasen

    }
}
