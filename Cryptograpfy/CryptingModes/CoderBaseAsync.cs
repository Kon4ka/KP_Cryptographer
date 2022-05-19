using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy.CryptModes
{
    public abstract class CoderBaseAsync
    {
        protected int _chipherPartSize = 8;

        protected CoderBaseAsync() { }

        public int GetCryptPartSize()
        {
            return _chipherPartSize;
        }

        abstract public Task<byte[]> Encrypt(byte[] DataBytes, int countOfThreads);
        abstract public Task<byte[]> Decrypt(byte[] DataBytes, int countOfThreads);
    }
}
