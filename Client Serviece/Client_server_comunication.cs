using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Google.Protobuf;
using gRPC.Server;
using KP_Crypt.Client_Serviece;
using KP_Crypt.Cryptograpfy;

namespace KP_Crypt.Client_Serviece
{
    class ClientServerComunication
    {
        private string _myName = "5000";
        private Crypto_Server.Crypto_ServerClient _client;   //private and_ & IDispose and ~
        private GrpcChannel _channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
        {
            HttpHandler = new GrpcWebHandler(new HttpClientHandler())
        });

        private string _name_of_another;
        private string _file_to_decrypt;
        private byte[] _frogKeyDecrypted;
        private byte[] _eGKeyDecrypted;

        public ClientServerComunication()
        {
            _client = new Crypto_Server.Crypto_ServerClient(_channel);
        }
        ~ClientServerComunication()
        {
            _channel.Dispose();
        }
        public async Task<string[]> FirstRegistrationAsync(string name) // try //to constructor/ lazy 
        {
            _myName = name;

            var response = await _client.SayHelloAsync(new HelloRequest { Name = _myName });// token
            return await IsAnyoneAtServerAsync();
        }

        public async Task<string[]> IsAnyoneAtServerAsync()
        {
            var others = (await _client.WhoAtServerAsync(new HelloRequest { Name = _myName })).Users.Split(',');
            others = others.Take(others.Length - 1).ToArray();
            return others;
        }


        public async Task<int> CleanDefaultDirAsync()
        {
            var clearing = await _client.ClearDirAsync(new HelloRequest { Name = _myName });
            if (clearing.IsClear == false)
                return -1;
            else return 0;
        }

        public async Task<int> SendEGKeyAsync(ulong[] keys)
        {
            //Создание первичного EG ключа
            string keyToSend = "";
            keyToSend = keys[0] + "," + keys[1] + ","+ keys[2] ;

            var sendEGKey = await _client.SendFileAsync(new FileBuffer
            {
                Filename = $"{_myName}.EGKey",
                Info = ByteString.CopyFrom(Encoding.Default.GetBytes(keyToSend))
            });
            if (sendEGKey.IsWrittenInServer != true)
                return -1;
            else return 0;
        }

        public async Task<int> SendFROGKeyAsync(byte[] toSend)        
        {
            var sendFROGKey = await _client.SendFileAsync(new FileBuffer
            {
                Filename = $"{_myName}.FROGKey",
                Info = ByteString.CopyFrom(toSend)
            });
            if (sendFROGKey.IsWrittenInServer != true)
                return -1;
            else return 0;
        }

        public async Task<byte[]> TakeEGKeyAsync(string another)
        {
            _name_of_another = another;
            var takeEGKey = await _client.TakeFileAsync(new WhatFile { Filename = $"{_name_of_another}.EGKey" });
            if (takeEGKey.Filename == "")
                return null;
            else
            {
                _eGKeyDecrypted = takeEGKey.Info.ToByteArray();
                return takeEGKey.Info.ToByteArray();
            }
        }
        public async Task<byte[]> TakeFROGKeyAsync(string another)
        {
            _name_of_another = another;
            var takeFROGKey = await _client.TakeFileAsync(new WhatFile { Filename = $"{_name_of_another}.FROGKey" });
            if (takeFROGKey.Filename == "")
                return null;
            else
            {
                return takeFROGKey.Info.ToByteArray();
            }

        }
        public async Task<byte[]> TakeFileAsync(string name)
        {
            var takeFile = await _client.TakeFileAsync(new WhatFile { Filename = name });
            if (takeFile.Filename == "" || name == null || name == "")    
                return null;
            else 
            { 
                return takeFile.Info.ToByteArray();  //Do state enum
            }
        }


    }
}
