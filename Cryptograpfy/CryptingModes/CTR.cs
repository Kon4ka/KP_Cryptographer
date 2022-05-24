using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy.CryptingModes
{
    public class CTR : CoderBase
    {
        private CoderBase _coder;
        private byte[] _initV;

        public CTR(CoderBase coder, byte[] IV)
        {
            _chipherPartSize = coder.GetCryptPartSize();
            _coder = coder;
            _initV = new byte[_chipherPartSize];
            Array.Copy(IV, _initV, _chipherPartSize);
        }

        public override byte[] Encrypt(byte[] inputInfo)
        {
            for (int i = 0; i < _chipherPartSize; i++)
                _initV[i] ^= (byte)i;
            byte[] outInfo = _coder.Encrypt(_initV);
            for (int i = 0; i < _chipherPartSize; i++)
                outInfo[i] ^= inputInfo[i];
            Array.Copy(outInfo, _initV, _chipherPartSize);
            return outInfo;
        }

        public override byte[] Decrypt(byte[] inputInfo)
        {
            for (int i = 0; i < _chipherPartSize; i++)
                _initV[i] ^= (byte)i;
            byte[] outInfo = _coder.Encrypt(_initV);
            for (int i = 0; i < _chipherPartSize; i++)
                outInfo[i] ^= inputInfo[i];
            Array.Copy(outInfo, _initV, _chipherPartSize);
            return outInfo;
        }

        protected override void KeyGeneration()
        {
            throw new NotImplementedException();
        }
    }
}
