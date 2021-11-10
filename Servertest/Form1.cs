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
using NAudio.Wave;
using Newtonsoft.Json;

namespace Servertest
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
            listener.Server.Close();
            ListenThread.Abort();
        }
        MessageModel memberinfo;
        Thread ListenThread;        
        TcpListener listener;
        List<Socket> clientList;        
        Dictionary<int, NetworkStream> networkStreams;
        List<Group> Groups = new List<Group>();        
        void Connect()//tạo kết nối
        {
            clientList = new List<Socket>();
            networkStreams = new Dictionary<int, NetworkStream>();
            IPAddress address = IPAddress.Parse("127.0.0.1");            
            listener = new TcpListener(address, 9981);            
            listener.Start();
            ListenThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Socket client = listener.AcceptSocket();
                        clientList.Add(client);

                        Thread receive = new Thread(() => Receive(client, clientList.Count));
                        receive.IsBackground = true;
                        receive.Start();
                    }
                }
                catch (Exception ex)
                {                    
                }
            });

            ListenThread.IsBackground = true;
            ListenThread.Start();

        }

        void Receive(Object obj, int count)
        {
            Socket client = obj as Socket;
            var networkStream = new NetworkStream(client);

            if (networkStreams.ContainsKey(count))
                networkStreams.Remove(count);
            networkStreams.Add(count, networkStream);

            byte[] data = new byte[1024 * 5000];
            //int sampleRate = 16000; // 16 kHz
            //int channels = 1; // mono
            //int bits = 16;

            //var recordingFormat = WaveFormat.CreateIeeeFloatWaveFormat(48000, 2);
            //var provider = new BufferedWaveProvider(recordingFormat);
            //WaveOut o = new WaveOut();            
            //o.Init(provider);
            //o.Volume = 1;            
            //o.Play();
            try

            {
                while (true)
                {
                    int recceived = networkStream.Read(data, 0, client.ReceiveBufferSize);
                    //provider.AddSamples(data, 0, recceived);

                    Parallel.ForEach(networkStreams, netw =>
                    {
                        if (netw.Key != count)
                        {
                            try
                            {
                                if (netw.Value.CanWrite)
                                    netw.Value.Write(data, 0, recceived);
                            }
                            catch(Exception ex)
                            {
                                networkStreams.Remove(netw.Key);
                            }
                        }
                            
                    });                    
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
            foreach (Member member in gr.SocketGroupList)
            {
                if (member.Id != message.ID)
                {
                    SendData(member.Socket, Serializer.Serialize(message));
                }
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {

        }


    }
}
