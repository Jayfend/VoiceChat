﻿using System;
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
                    
                    memberinfo = Deserialize(data);
                    if (memberinfo.Message != null)
                    {
                        string s = memberinfo.Name + ": " + memberinfo.Message;
                        AddMessage(s);
                    }
                    if (memberinfo.Audio_ID != null)
                    { 
                        AudioDbContext db = new AudioDbContext();
                        var query = from m in db.Audios
                                    where memberinfo.Audio_ID == m.ID.ToString()
                                    select m;
                        foreach (var audiodata in query)
                        {
                            data = TrimEnd(audiodata.AudioData);
                            AudioPlayer audioPlayer = new AudioPlayer();
                            audioPlayer.PlayAudio(data);
                        }

                    }
                   
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
            
            var objToString = JsonConvert.SerializeObject(obj);
           
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, objToString);
            return stream.ToArray();
        }
        MessageModel Deserialize(byte[] data) // gộp mảnh
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            var stringObj =(string)formatter.Deserialize(stream);
            // make sure you use same type what you use chose during conversation

            return JsonConvert.DeserializeObject<MessageModel>(stringObj);

        }

        void AddMessage(String s)
        {
            lsvMessage.Items.Add(new ListViewItem() { Text = s });
            txtMessage.Clear();
        }
        private void BtnSend_Click(object sender, EventArgs e)
        {
            foreach (Socket item in clientList)
            {
                Send(item);
            }
            
        }
        void Send(Socket client)
        {
            MessageModel member;
            member = new MessageModel(0,0, null, "Server");
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
