using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using Newtonsoft.Json;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()//comment
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
        }

        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {
            Close();
        }
        MessageModel memberinfo;
        MessageModel member;
        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;
        void Connect()//tạo kết nối
        {
            clientList = new List<Socket>();
            IP = new IPEndPoint(IPAddress.Any, 7749);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            server.Bind(IP);
            Thread Listen = new Thread(() =>
            {
                try
                {


                    while (true)
                    {
                        server.Listen(100);
                        Socket client = server.Accept();
                        clientList.Add(client);

                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start(client);
                    }
                }
                catch
                {
                    IP = new IPEndPoint(IPAddress.Any, 7749);
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                }
            });

            Listen.IsBackground = true;
            Listen.Start();

        }
        
        void Receive(Object obj)
        {
            Socket client = obj as Socket;
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];                    
                    client.Receive(data);
                    data = TrimEnd(data);

                    
                    AudioPlayer audioPlayer = new AudioPlayer();
                    audioPlayer.PlayAudio(data);

                    //doan nay sau khi apply sql se lam tiep
                    /*
                    memberinfo = Deserialize(data);                    
                    string s = memberinfo.Name + ": " + memberinfo.Message;
                    AddMessage(s);
                    */
                }
            }
            catch (Exception ex)
            {
                clientList.Remove(client);
                client.Close();
            }
        }
        byte[] Serialize(object obj) // phân mảnh
        {
            // proper way to serialize object
            var objToString = JsonConvert.SerializeObject(obj);
            // convert that that to string with ascii you can chose what ever encoding want
            return System.Text.Encoding.ASCII.GetBytes(objToString);
        }
        MessageModel Deserialize(byte[] data) // gộp mảnh
        {
            // make sure you use same type what you use chose during conversation
            var stringObj = System.Text.Encoding.ASCII.GetString(data);
            // proper way to Deserialize object
            return JsonConvert.DeserializeObject<MessageModel>(stringObj);

        }

        void AddMessage(String s)
        {
            lsvMessage.Items.Add(new ListViewItem() { Text = s });
        }
        private void BtnSend_Click(object sender, EventArgs e)
        {
            foreach (Socket item in clientList)
            {
                Send(item);
            }
            txtMessage.Clear();
        }
        void Send(Socket client)
        {
            member.Message =txtMessage.Text;
            if (member.Message != String.Empty)
                client.Send(Serialize(member));
        }

        public byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }
    }
}
