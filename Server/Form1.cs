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
using Models.Functions;
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
            server.Close();
            ListenThread.Abort();
        }
        MessageModel memberinfo;
        Thread ListenThread;
        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;
        List<Group> Groups = new List<Group>();
        void Connect()//tạo kết nối
        {
            clientList = new List<Socket>();
            IP = new IPEndPoint(IPAddress.Any, 7749);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            server.Bind(IP);
            ListenThread = new Thread(() =>
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

            ListenThread.IsBackground = true;
            ListenThread.Start();
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
                    memberinfo = Serializer.Deserialize(data);
                    if (memberinfo.VoiceData != null)
                    {
                        using (var db = new AudioDbContext())
                        {
                            var Audioitem = new Audio() { AudioData = memberinfo.VoiceData };
                            db.Audios.Add(Audioitem);
                            db.SaveChanges();
                            memberinfo.Audio_ID = Audioitem.ID.ToString();
                            SendData(client, Serializer.Serialize(memberinfo));
                        }
                    }
                 
                    Group gr = Groups.FirstOrDefault(g => g.ID == memberinfo.Group_ID);
                    if(gr != null)
                    {
                        if(gr.SocketGroupList.Any(c => c.Id == memberinfo.ID) == false)
                        {
                            gr.SocketGroupList.Add(new Member
                            {
                                Id = memberinfo.ID,
                                Name = memberinfo.Name,
                                Socket = client
                            });

                            MessageModel welcomeMessage = new MessageModel
                            {
                                Group_ID = memberinfo.Group_ID,
                                Message = $"{memberinfo.Name} joined group!",
                                Name = $"Group {memberinfo.Group_ID}"                                
                            };
                            GroupSend(gr, welcomeMessage);
                        }
                    }
                    else
                    {
                        gr = new Group
                        {
                            ID = memberinfo.Group_ID,
                            SocketGroupList = new List<Member>
                            {
                                new Member
                                {
                                    Id = memberinfo.ID,
                                    Name = memberinfo.Name,
                                    Socket = client
                                }
                            }
                        };                        
                        Groups.Add(gr);
                        MessageModel welcomeMessage = new MessageModel
                        {
                            Group_ID = memberinfo.Group_ID,
                            Message = $"Group {memberinfo.Group_ID} created!",
                            Name = $"Group {memberinfo.Group_ID}"
                        };
                        GroupSend(gr, welcomeMessage);
                    }

                    if (memberinfo.Message != null)
                    {
                        string s = memberinfo.Name + ": " + memberinfo.Message;
                        AddMessage(s);
                    }
                    GroupSend(gr, memberinfo);
                }
                
            }
            catch (Exception ex)
            {              
                
            }
        }        

        void AddMessage(String s)
        {
            lsvMessage.Items.Add(new ListViewItem() { Text = s });
            txtMessage.Clear();
        }

        private static void SendData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            while (total < size)
            {
                sent = s.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
        }

        public byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }
        public void GroupSend(Group gr, MessageModel message)
        {
            List<int> idToRemove = new List<int>();
            foreach(Member member in gr.SocketGroupList)
            {
                if (member.Socket.Connected)
                {
                    if (member.Id != message.ID)
                    {
                        SendData(member.Socket, Serializer.Serialize(message));
                    }
                }
                else
                {
                    idToRemove.Add(member.Id);
                }
            }

            gr.SocketGroupList = gr.SocketGroupList.Where(g => idToRemove.Contains(g.Id) == false).ToList();
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            MessageModel member;
            member = new MessageModel(0, 0, null, "Server");
            member.Message = txtMessage.Text;
            AddMessage(member.Name + ": " + member.Message);
            if (member.Message != String.Empty)
                foreach (Socket item in clientList)
                {
                    SendData(item, Serializer.Serialize(member));
                    txtMessage.Clear();
                }
        }
    }
}
