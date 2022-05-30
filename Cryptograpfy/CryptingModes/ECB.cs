using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Crypt.Cryptograpfy.CryptModes
{

    public class ECB : CoderBaseAsync
    {
        private CoderBase _coder;

        public ECB(CoderBase coder)
        {
            _chipherPartSize = coder.GetCryptPartSize();
            _coder = coder;
        }

        private Task MakeEncryptTask(byte[] inBuf, byte[] outBuf, int offset, int blocksCount)
        {
            return Task.Run(() =>
            {
                _coder.Encrypt(inBuf, outBuf, offset, blocksCount);
            });
        }

        private Task MakeDecryptTask(byte[] inBuf, byte[] outBuf, int offset, int blocksCount)
        {
            return Task.Run(() =>
            {
                _coder.Decrypt(inBuf, outBuf, offset, blocksCount);
            });
        }

        public override async Task<byte[]> Encrypt(byte[] inputBytes, int countOfThreads = 4) //-countOfThreads
        {
            if (inputBytes.Length == 0)
                throw new ArgumentException("Data is empty");

            int blocksCount = inputBytes.Length / _chipherPartSize;

            byte[] result = new byte[blocksCount * _chipherPartSize];

            int blocksToOneThread = blocksCount / countOfThreads;
            int blocksToLastThread = blocksToOneThread + blocksCount % countOfThreads;

            Task[] encryptTasks = new Task[countOfThreads];

            var resultall = new List<byte[]>(blocksCount);
            for (int i = 0; i < blocksCount; i++)
                resultall.Add(new byte[_chipherPartSize]);
            

            Parallel.For(0, blocksCount,
                i => resultall[i] = _coder.Encrypt(inputBytes, result, i * 1 * _chipherPartSize, 1));

            return resultall.SelectMany(s => s).ToArray();
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

            var resultall = new List<byte[]>(blocksCount);
            for (int i = 0; i < blocksCount; i++)
                resultall.Add(new byte[_chipherPartSize]);

            Parallel.For(0, blocksCount,
                i => resultall[i] = _coder.Decrypt(inputBytes, result, i * 1 * _chipherPartSize, 1));

            return resultall.SelectMany(s => s).ToArray();
        }
    }
}
