using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Google.Protobuf;
using gRPC.Server;
using KP_Crypt.Client_Serviece;
using KP_Crypt.Cryptograpfy;
using KP_Crypt.Cryptograpfy.CryptModes;
using System.Collections.Generic;
using System.Threading;

namespace KP_Crypt
{
    public partial class Form1 : Form
    {
        public Form1(string _name)
        {
            InitializeComponent();
            _myName = _name;
        }
        ~Form1()
        {
            channel.Dispose();
        }

        private HttpClientHandler unsafeHandler = new HttpClientHandler();

        private string _myName = "5000";
        Crypto_Server.Crypto_ServerClient client;
        private CryptographyEngine cryptography;
        GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
        {
            HttpHandler = new GrpcWebHandler(new HttpClientHandler())
        });
        private string _name_of_another;
        private CryptModesEn _mode = CryptModesEn.ECB;
        private ClientServerComunication _client = new ClientServerComunication();
        private CancellationToken token;

        private void Form1_Load(object sender, EventArgs e)
        {
            token = new CancellationToken();
            client = new Crypto_Server.Crypto_ServerClient(channel);
            cryptography = CryptographyEngine.GetCryptographyEngine();
            label5.Text += " " + _myName;
            button1_Click(sender, e);   //TODO
        }

        private async void button1_Click(object sender, EventArgs e)//async добавить, cancelation token
        {
            var others =  await _client.FirstRegistrationAsync(_myName, token);

            if (others.Length == 0)
            {
                label1.Text = "Пока вы на сервере один, ждите появления другого пользователя.";

                var isClean = await _client.CleanDefaultDirAsync(token);
                if (isClean == -1)
                {
                    MessageBox.Show("Сервер не готов к работе...");
                    Application.Exit();
                }

                ulong[] keys = cryptography.GetEGKeys();
                int IsWrittenOnServer = await _client.SendEGKeyAsync(keys, token);
                if (IsWrittenOnServer == -1)
                    label2.Text = "Не удалось отослать файл на сервер";
            }
            else
            {
                //Создание EG ключа

                ulong[] keys = cryptography.GetEGKeys();
                int IsWrittenOnServer = await _client.SendEGKeyAsync(keys, token);
                if (IsWrittenOnServer == -1)
                    label2.Text = "Не удалось отослать файл на сервер";

                _name_of_another = others[0];
                byte[] IsTaken = await _client.TakeEGKeyAsync(_name_of_another, token);
                if (IsTaken == null)
                    label2.Text = $"Не нашли файла ${_name_of_another}.EGKey.txt";
                else
                {
                    string[] keysEG = Encoding.Default.GetString(IsTaken).Split(',');
                    cryptography.SetEGKeysStr(keysEG);
                }
                cryptography.FROGKeyGeneration();
                string keyAndIV = cryptography.frogKeyString + cryptography.frogIVectorString;
                // Шифрование ключа ElGamal'ом
                byte[] toSend = cryptography.CryptWithEiGamal(Encoding.Default.GetBytes(keyAndIV));
                int IsSend = await _client.SendFROGKeyAsync(toSend, token);
                if (IsSend == -1)
                    label2.Text = "Не удалось записать файл на серваке...";
            }
        }

        private async void button5_Click(object sender, EventArgs e)    //Показать список файлов на серваке
        {
            try
            {
                var taking = (await client.TakeAllFileNamesAsync(new HelloRequest { Name = _myName })).Files.Split(',');
                listBox1.Items.Clear();
                foreach (var f in taking)
                    listBox1.Items.Add(f);
            }
            catch 
            {
                label1.Text = "Сервер не отвечает или не запущен...";
            }

        }

        private async void button7_Click(object sender, EventArgs e)        //Take FROG
        {
            var others = await _client.IsAnyoneAtServerAsync(token);
            if (others.Length == 0)
            {
                MessageBox.Show("Вы одни в сети или зашифрованный ключ FROG еще не сформирован другой стороной... Попробуйте позже");
                return;
            }
            _name_of_another = others[0];

            var takeFROGKey = await _client.TakeFROGKeyAsync(_name_of_another, token);
            if (takeFROGKey == null)
                label2.Text = $"Не нашли файла {_name_of_another}.FROGKey.txt";
            else
            {
                //Расшифровка ключа
                byte[] fKeyAndVIByte = takeFROGKey;
                var tmp = cryptography.UnCryptWithEiGamal(fKeyAndVIByte);
                string[] fKeyAndVI = (Encoding.Default.GetString(cryptography.UnCryptWithEiGamal(fKeyAndVIByte))).Split(' ');
                //_frogKeyDecrypted = Encoding.Default.GetBytes(fKeyAndVI[0]);

                cryptography.frogKeyString = fKeyAndVI[0];
                cryptography.frogIVectorString = fKeyAndVI[2];  //TODO
                if (Encoding.Default.GetBytes(fKeyAndVI[2]).Length < 16)
                    cryptography.frogKeyString = fKeyAndVI[0];
                cryptography.CreateFROG(Encoding.Default.GetBytes(fKeyAndVI[0]), Encoding.Default.GetBytes(fKeyAndVI[2]));

                label2.Text = $"Нашли файл {_name_of_another}.FROGKey.txt, \nВыберите файл для зашифровки и отправки выше.";
                label1.Text = "";
            }
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var response = await client.SayByeAsync(new HelloRequest { Name = _myName });
        }

        private async void button2_Click(object sender, EventArgs e)      //Выберите файл
        {
            openFileDialog1.Filter = "All files(*.*)|*.*";
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            byte[] fileTextByte = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
            if (fileTextByte.Length == 0)
            {
                MessageBox.Show("Вы выбрали пустой файл. Выберите другой");
                return;
            }
            label3.Text = "Файл: " + filename + " открыт и готов для шифрования.";

            //Шифрование FROG'ом
            byte[] encryptedFile = EncryptFile(fileTextByte).Result;   //progress bar 

            if (encryptedFile is null)
            {
                MessageBox.Show("У вас нет необходимых ключей для шифрования.");
                return;
            }
            label3.Text = "Файл: " + filename + " зашифрован.";
            //И передача его серваку
            try
            {
                var sending = await client.SendFileAsync(new FileBuffer { Filename = $"{_myName}.Mess.{filename}", Info = ByteString.CopyFrom(encryptedFile) });
                label3.Text = "Файл: " + filename + " зашифрован и отправлен на сервер.";
            }
            catch
            {
                MessageBox.Show("Файл слишком велик или неподходящего формата");
            }

        }

        private async void button6_Click(object sender, EventArgs e)  //Take EIGamal
        {
            var others = await _client.IsAnyoneAtServerAsync(token);
            if (others.Length == 0)
            {
                MessageBox.Show("Вы одни в сети... Попробуйте позже");
                return;
            }
            _name_of_another = others[0];


            var takeEGKey = await _client.TakeEGKeyAsync(_name_of_another, token);
            if (takeEGKey == null)
                label2.Text = $"Не нашли файла ${_name_of_another}.EGKey.txt";
            else
            {
                string[] keys = Encoding.Default.GetString(takeEGKey).Split(',');
                cryptography.SetEGKeysStr(keys);

                cryptography.FROGKeyGeneration();
                string keyAndIV = cryptography.frogKeyString + cryptography.frogIVectorString;
                // Шифрование ключа ElGamal'ом
                byte[] toSend = cryptography.CryptWithEiGamal(Encoding.Default.GetBytes(keyAndIV));

                int IsSend = await _client.SendFROGKeyAsync(toSend, token);
                label2.Text = $"Нашли файл ${_name_of_another}.EGKey.txt, Генерируем файл \nс ключом FROG и отправляем на сервер";
                if (IsSend == -1)
                    label2.Text = "Не удалось записать файл на серваке...";

            }
        }

        private async void button8_Click(object sender, EventArgs e)      //Расшифровать файл
        {
            button5_Click(sender, e);
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Обновите список файлов в листбоксе и выберите файл содержащий \"Mess\" - файл сообщения.");
                return;
            }
            string filename = listBox1.SelectedItem.ToString();
            if (filename.Contains(".Mess"))
            {
                //List<byte> tmp = new List<byte>;
                byte[] IsSend = await _client.TakeFileAsync(filename, token);
                try
                {
                    var decryptedText = DecryptFile(IsSend).Result;
                    string path = Directory.GetCurrentDirectory();
                    if (!Directory.Exists(path + "/Messages/"))
                    {
                        Directory.CreateDirectory(path + "/Messages/");
                    }
                    if (decryptedText is null)
                    {
                        MessageBox.Show("Отсутствуют необходимые ключи для расшифровки. Попробуйте загрузить с сервера ключ FROG");
                        return;
                    }
                    File.WriteAllBytes(path + "/Messages/" + filename + ".txt", decryptedText);
                    label2.Text = "Файл: " + filename + " сохранён в папку:\n" + path + "\\Messages\\";
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при попытке расшифровать файл.");
                }
            }
            else
            {
                MessageBox.Show("Вы выбрали не файл сообщения, выберите файл содержащий Mess");
            }
        }
        /// <summary>
        /// Encrypting
        /// </summary>
        /// <param name="fileInfo">file</param>
        /// <returns>some</returns>
        public async Task<byte[]> EncryptFile(byte[] fileInfo)
        {
            byte[] filePart = await cryptography.CryptWithFROGAsync(fileInfo, CryptModesEn.ECB);
            return filePart;
        }

        public async Task<byte[]> DecryptFile(byte[] fileInfo)
        {
            byte[] filePart = await cryptography.UnCryptWithFROGAsync(fileInfo, CryptModesEn.ECB);
            return filePart;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedItem)
            {
                case "ECB": _mode = CryptModesEn.ECB; break;
                case "CBC": _mode = CryptModesEn.CBC; break;
                case "CFB": _mode = CryptModesEn.CFB; break;
                case "CTR": _mode = CryptModesEn.CTR; break;
                case "OFB": _mode = CryptModesEn.OFB; break;
                case "RD" : _mode = CryptModesEn.CBC; break;
                case "RDH": _mode = CryptModesEn.CBC; break;
            }
        }
    }
}
