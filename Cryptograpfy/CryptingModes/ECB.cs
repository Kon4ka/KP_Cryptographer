using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy.CryptModes
{

    public class ECB : CoderBaseAsync
    {
        private CoderBase _encoder;

        public ECB(CoderBase encoder)
        {
            _chipherPartSize = encoder.GetCryptPartSize();
            _encoder = encoder;
        }

        private Task MakeEncryptTask(byte[] inBuf, byte[] outBuf, int offset, int blocksCount)
        {
            return Task.Run(() =>
            {
                _encoder.Encrypt(inBuf, outBuf, offset, blocksCount);
            });
        }

        private Task MakeDecryptTask(byte[] inBuf, byte[] outBuf, int offset, int blocksCount)
        {
            return Task.Run(() =>
            {
                _encoder.Decrypt(inBuf, outBuf, offset, blocksCount);
            });
        }

        public override async Task<byte[]> Encrypt(byte[] inputBytes, int countOfThreads = 4)
        {
            if (inputBytes.Length == 0)
                throw new ArgumentException("Data is empty");

            int blocksCount = inputBytes.Length / _chipherPartSize;

            byte[] result = new byte[blocksCount * _chipherPartSize];

            int blocksToOneThread = blocksCount / countOfThreads;
            int blocksToLastThread = blocksToOneThread + blocksCount % countOfThreads;

            Task[] encryptTasks = new Task[countOfThreads];

            for (int i = 0; i < countOfThreads; i++)
            {
                if (i == countOfThreads - 1)
                {
                    encryptTasks[i] = MakeEncryptTask(inputBytes, result,
                        i * blocksToOneThread * _chipherPartSize, blocksToLastThread);
                }
                else
                {
                    encryptTasks[i] = MakeEncryptTask(inputBytes, result,
                        i * blocksToOneThread * _chipherPartSize, blocksToOneThread);
                }
            }

            await Task.WhenAll(encryptTasks);

            return result;
        }

        public override async Task<byte[]> Decrypt(byte[] inputBytes, int countOfThreads = 4)
        {
            if (inputBytes.Length == 0)
                throw new ArgumentException("Data is empty");

            int blocksCount = inputBytes.Length / _chipherPartSize;

            byte[] result = new byte[blocksCount * _chipherPartSize];

            int blocksToOneThread = blocksCount / countOfThreads;
            int blocksToLastThread = (blocksToOneThread + blocksCount) % countOfThreads;
            Task[] decryptTasks = new Task[countOfThreads];

            for (int i = 0; i < countOfThreads; i++)
            {
                if (i == countOfThreads - 1)
                {
                    decryptTasks[i] = MakeDecryptTask(inputBytes, result,
                        i * blocksToOneThread * _chipherPartSize, blocksToLastThread);
                }
                else
                {
                    decryptTasks[i] = MakeDecryptTask(inputBytes, result,
                        i * blocksToOneThread * _chipherPartSize, blocksToOneThread);
                }
            }

            await Task.WhenAll(decryptTasks);

            return result;
        }
    }
}
