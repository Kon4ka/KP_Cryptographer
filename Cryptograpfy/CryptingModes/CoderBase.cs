using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy
{
    public enum CryptModesEn
    {
        ECB, CBC, CFB, OFB, CTR, RD, RDH
    }
    public abstract class CoderBase
    {
        protected int _chipherPartSize = 8;

        abstract protected void KeyGeneration();

        abstract public byte[] Encrypt(byte[] infoBytes);
        abstract public byte[] Decrypt(byte[] infoBytes);
        protected CoderBase() {}

        public int GetCryptPartSize()
        {
            return _chipherPartSize;
        }

        public bool Encrypt(byte[] infoBytes, byte[] outputBuffer, int offset, int countBlocks)
        {
            for (int i = 0; i < countBlocks; i++)
            {
                byte[] currentInfoBytes = new byte[_chipherPartSize];
                Array.Copy(infoBytes, offset + (i * _chipherPartSize), currentInfoBytes, 0, currentInfoBytes.Length);

                byte[] currentOutputBuffer = Encrypt(currentInfoBytes);
                Array.Copy(currentOutputBuffer, 0, outputBuffer, offset + (i * _chipherPartSize), currentOutputBuffer.Length);
            }
            return true;
        }

        public bool Decrypt(byte[] infoBytes, byte[] outputBuffer, int offset, int countBlocks)
        {
            for (int i = 0; i < countBlocks; i++)
            {
                byte[] currentInfoBytes = new byte[_chipherPartSize];
                Array.Copy(infoBytes, offset + (i * _chipherPartSize), currentInfoBytes, 0, currentInfoBytes.Length);

                byte[] currentOutputBuffer = Decrypt(currentInfoBytes);
                Array.Copy(currentOutputBuffer, 0, outputBuffer, offset + (i * _chipherPartSize), currentOutputBuffer.Length);
            }
            return true;
        }

        public abstract class CoderAsyncBase 
        {

            protected int _chipherPartSize = 8;

            protected CoderAsyncBase()
            { }

            public int GetSizeofCipherPie()
            {
                return _chipherPartSize;
            }

            abstract public Task<byte[]> Encrypt(byte[] infoBytes, int countOfThreads);
            abstract public Task<byte[]> Decrypt(byte[] infoBytes, int countOfThreads);
        }
    }
}
