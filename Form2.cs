using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

namespace KP_Crypt
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            });
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
 
            var client = new Crypto_Server.Crypto_ServerClient(channel);
            string valid_name = "";

            if (textBox1.Text == "")
                MessageBox.Show("Введите имя пользователя.");
            else
            {
                try
                {
                    valid_name = (await client.SayHelloAsync(new HelloRequest { Name = textBox1.Text })).Message;
                }
                catch
                {
                    MessageBox.Show("Сервер недоступен");
                    return;
                }
                if (valid_name == "Wrong name!")
                    MessageBox.Show("Некорректное имя пользователя. Введите другое.");
                else if (valid_name == "More than two")
                {
                    MessageBox.Show("В системе уже есть два пользователя. Дождитесь выхода кого-то из них.");
                }
                else
                {
                    Form1 Chat = new Form1(textBox1.Text);
                    Chat.Show();
                    this.Visible = false;
                }
                    
            }

        }
    }
}
