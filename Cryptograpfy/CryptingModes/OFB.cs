using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy.CryptingModes
{
    public class OFB : CoderBase
    {
        private CoderBase _coder;
        private byte[] _initV;

        public OFB(CoderBase coder, byte[] IV)
        {
            _chipherPartSize = coder.GetCryptPartSize();
            _coder = coder;
            _initV = new byte[_chipherPartSize];
            Array.Copy(IV, _initV, _chipherPartSize);
        }

        public override byte[] Encrypt(byte[] inputInfo)
        {
            byte[] outInfo = _coder.Encrypt(_initV);
            Array.Copy(outInfo, _initV, _chipherPartSize);
            for (int i = 0; i < _chipherPartSize; i++)
                outInfo[i] ^= inputInfo[i];

            return outInfo;
        }

        public override byte[] Decrypt(byte[] inputInfo)
        {
            byte[] outInfo = _coder.Encrypt(_initV);
            Array.Copy(outInfo, _initV, _chipherPartSize);
            for (int i = 0; i < _chipherPartSize; i++)
                outInfo[i] ^= inputInfo[i];

            return outInfo;
        }

        protected override void KeyGeneration()
        {
            throw new NotImplementedException();
        }
    }
}
