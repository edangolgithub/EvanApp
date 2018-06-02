using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EvanApp.Droid.Pages.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatClient : ContentPage
    {
       

        TcpClient client;
        NetworkStream NetStream;
        //AsyncCallback aCallback;
        //Socket socket;
        BinaryReader reader;
        //private Socket handler;
        BinaryWriter writer;
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            NetStream = client.GetStream();
            writer.Write("exit");
        }
        public ChatClient()
        {
            InitializeComponent();
            serverbtn.Clicked += Serverbtn_Clicked;
            sendbtn.Clicked += sendd;
            serverbtncls.Clicked += Serverbtncls_Clicked;
            navbtn.Clicked += ((o, e) => {

                Navigation.PushAsync(new MainPage());
            });
        }

        private void Serverbtncls_Clicked(object sender, EventArgs e)
        {
            try
            {
                NetStream = client.GetStream();
                writer.Write("Connection Closed");
                writer.Close();
                reader.Close();
                NetStream.Close();
                client.Close();
                Task.Run(() => DisplayAlert("error", "Connection Closed", "ok"));
            }
            catch (Exception es)
            {
                Task.Run(() => DisplayAlert("error", es.Message, "ok"));
            }
        }

        private void sendd(object sender, EventArgs e)
        {
            Task.Run(() => SendMessage());
        }

        private void SendMessage()
        {
            try
            {
                NetStream = client.GetStream();
                writer.Write(editor.Text);
                writer.Flush();

                Device.BeginInvokeOnMainThread(() =>
                {
                    editor.Text = "";
                    msglabel.Text = "message Sent to Server";
                });

                Task.Run(() => DisplayAlert("Success", "Message Sent", "ok"));
            }
            catch (Exception es)
            {
                Task.Run(() => DisplayAlert("error", es.Message, "ok"));
            }
        }

        private void Serverbtn_Clicked(object sender, EventArgs e)
        {
            Task.Run(() => ConnectToserver());
        }

        private void ConnectToserver()
        {
            try
            {
                client = new TcpClient();

                client.Connect("192.168.1.114", 4444);
                NetStream = client.GetStream();
                Task.Run(() => DisplayAlert("Success", "Connected To Tcp Server at port 4444", "ok"));
                writer = new BinaryWriter(NetStream);
                reader = new BinaryReader(NetStream);
                writer.Write("Connection came from " + this.ToString());
                string data = "";
                do
                {
                    try
                    {
                        data = reader.ReadString();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            msglabel.Text = data;
                        });

                    }
                    catch (Exception e)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            msglabel.Text = e.Message;
                        });
                    }
                } while (data != "q");

            }
            catch (Exception ex)
            {
                Task.Run(() => DisplayAlert("message", ex.Message, "ok"));
                Device.BeginInvokeOnMainThread(() =>
                {
                    msglabel.Text = ex.Message;
                });
            }
        }


    }
}
