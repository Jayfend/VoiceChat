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
using Newtonsoft.Json;
using System.Windows.Forms;
using Models;
using NAudio.Wave;
using NAudio.Utils;
using Models.Functions;
using System.Diagnostics;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
      
            
        }
        IPEndPoint IP;
        Socket Client;
        MessageModel memberinfo;
        Thread listenThread;
        Stopwatch stopwatch = new Stopwatch();
        CallStatus CallStatus;
        CallHandler callHandler;
        Thread callThread;
        void Connect()//tạo kết nối
        {
            
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"),7749);
            Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                Client.Connect(IP);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listenThread = new Thread(Receive);
            listenThread.IsBackground = true;
            listenThread.Start();
        }
        void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];

                    Client.Receive(data);
                     memberinfo = Serializer.Deserialize(data);
                    switch (memberinfo.MessageType)
                    {
                        case MessageType.Text:
                            if (!string.IsNullOrEmpty(memberinfo.Message))
                            {
                                string s = memberinfo.Name + ": " + memberinfo.Message;
                                AddMessage(lsvMessage, s, Color.FromArgb(136, 224, 239), Color.White);
                                AddMessage(listMyMessage, "", Color.Transparent, Color.Transparent);
                            }
                            if (!string.IsNullOrEmpty(memberinfo.Audio_ID))
                            {
                                AddVoice(lsvMessage, memberinfo, Color.FromArgb(136, 224, 239), Color.White);
                                AddVoice(listMyMessage, null, Color.Transparent, Color.Transparent);
                            }
                            break;
                        case MessageType.Call:
                            if(CallStatus != CallStatus.Calling)
                            {
                                CallRequest callreq = new CallRequest(memberinfo.Name);
                                var dialogResult = callreq.ShowDialog();

                                if (dialogResult == DialogResult.Yes)
                                {
                                    MessageModel member;
                                    member = new MessageModel
                                    {
                                        Group_ID = int.Parse(txtGroupID.Text),
                                        ID = int.Parse(txtID.Text),
                                        MessageType = MessageType.CallAccepted,
                                        Name = txtName.Text
                                    };
                                    Client.Send(Serializer.Serialize(member));
                                    this.BeginInvoke(new MethodInvoker(delegate
                                    {
                                        StartCall(CallStatus.Calling);
                                    }));

                                }
                            }
                            break;


                        case MessageType.CallAccepted:
                            if(CallStatus == CallStatus.Pending)
                            {
                                this.BeginInvoke(new MethodInvoker(delegate
                                {
                                    StartCall(CallStatus.Calling);
                                }));                                
                            }
                            break;
                    }

                }
            }
            catch
            {
                //error
            }
        }
        void Send()
        {
            MessageModel member;
            member = new MessageModel(int.Parse(txtID.Text), int.Parse(txtGroupID.Text), null, txtName.Text);
            member.Message = txtMessage.Text;
            if (member.Message != String.Empty)
                Client.Send(Serializer.Serialize(member));
        }
        void RequestCall()
        {
            CallStatus = CallStatus.Pending;

            MessageModel member;
            member = new MessageModel
            {
                Group_ID = int.Parse(txtGroupID.Text),
                ID = int.Parse(txtID.Text),
                MessageType = MessageType.Call,
                Name = txtName.Text                
            };
            Client.Send(Serializer.Serialize(member));
        }        
        
        void AddVoice(ListView listView, MessageModel message, Color backColor, Color textColor)
        {       
            if(message != null)
            {
                listView.Items.Add(new ListViewItem()
                {
                    Text = $"[>Voice: {message.Name}<]",
                    Tag = message.Audio_ID,
                    ImageIndex = 0,
                    BackColor = backColor,
                    ForeColor = textColor
                });
            }
            else
            {
                listView.Items.Add(new ListViewItem(""));
            }
        }
        void AddMessage(ListView messages, String s, Color backColor, Color textColor)
        {
            if (!string.IsNullOrEmpty(s))
            {
                messages.Items.Add(new ListViewItem()
                {
                    Text = s,
                    ImageIndex = 1,
                    BackColor = backColor,
                    ForeColor = textColor                    
                });                
            }
            else
            {
                messages.Items.Add(new ListViewItem()
                {
                    Text = ""
                });
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            MessageModel member = new MessageModel(int.Parse(txtID.Text), int.Parse(txtGroupID.Text), null, txtName.Text);
            if (txtName.Text == string.Empty || txtID.Text == string.Empty || txtGroupID.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            CheckForIllegalCrossThreadCalls = false;
            Connect();

            Client.Send(Serializer.Serialize(member));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(txtMessage.Text))
            {
                Send();
                AddMessage(listMyMessage, txtName.Text + ": " + txtMessage.Text, Color.FromArgb(247, 164, 64), Color.White);
                AddMessage(lsvMessage, "", Color.Transparent, Color.Transparent);
                txtMessage.Clear();
            }
        }

        private void btnvoice_Click(object sender, EventArgs e)
        {
            MessageModel member;
            member = new MessageModel(int.Parse(txtID.Text), int.Parse(txtGroupID.Text), null, txtName.Text);
            Recorder recorder = new Recorder();
            var dialogResult = recorder.ShowDialog();

            if(dialogResult == DialogResult.OK)
            {
                MemoryStream stream = recorder.memoryStream;
                byte[] voiceData = stream.ToArray();


                //Luu byte[] vao database
                using ( var db = new AudioDbContext())
                    {
                    var Audioitem = new Audio() { AudioData = voiceData };
                    db.Audios.Add(Audioitem);
                    db.SaveChanges();
                    member.Audio_ID = Audioitem.ID.ToString();
                    
                }
                AddVoice(listMyMessage, member, Color.FromArgb(247, 164, 64), Color.White);
                AddVoice(lsvMessage ,null, Color.Transparent, Color.Transparent);
                Client.Send(Serializer.Serialize(member));
            }
            else
            {
                MessageBox.Show("Voice audio cancelled");
            }
            
        }

        public byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lsvMessage_Click(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            if(listView.SelectedItems.Count > 0)
            {
                string voiceId = listView.SelectedItems[0].Tag?.ToString();
                if (!string.IsNullOrEmpty(voiceId))
                {
                    using(AudioDbContext db = new AudioDbContext())
                    {
                        var query = from m in db.Audios
                                    where voiceId == m.ID.ToString()
                                    select m;
                        foreach (var audiodata in query)
                        {
                            byte[] data = TrimEnd(audiodata.AudioData);
                            AudioPlayer audioPlayer = new AudioPlayer();
                            audioPlayer.PlayAudio(data);
                        }
                    }                    
                    
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(listenThread != null)
                listenThread.Abort();
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SendMessage();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            RequestCall();
            StartCall(CallStatus.Pending);
        }
        void StartCall(CallStatus callStatus)
        {
            CallStatus = callStatus;
            btnCall.Enabled = false;
            btnEndCall.Visible = true;
            lbCallTime.Visible = true;
            stopwatch.Restart();
            stopwatch.Start();
            timerCall.Start();

            if(callStatus == CallStatus.Calling)
            {
                callHandler = new CallHandler();
                callHandler.connectionFailHandler = CallConnectFailed;
                callHandler.Start();
                //callThread = new Thread(callHandler.Start);
                //callThread.IsBackground = true;                                
                //callThread.Start();
            }            
        }

        void EndCall()
        {
            btnCall.Enabled = true;
            TimeSpan timeSpan = TimeSpan.FromSeconds(stopwatch.Elapsed.TotalSeconds);
            listMyMessage.Items.Add($"Call ended. Time total: {timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}");
            timerCall.Stop();
            stopwatch.Stop();
            lbCallTime.Visible = false;
            lbCallTime.Text = "0:0:0";
            btnEndCall.Visible = false;
            CallStatus = CallStatus.Ended;

            if(callHandler != null)
            {
                callHandler.Dispose();
                callHandler = null;
            }
        }

        private void CallConnectFailed()
        {
            listMyMessage.Items.Add($"Could not connect to the call server!");
            EndCall();
        }

        private void timerCall_Tick(object sender, EventArgs e)
        {
            //Hien thi thoi gian ghi audio
            TimeSpan timeSpan = TimeSpan.FromSeconds(stopwatch.Elapsed.TotalSeconds);
            lbCallTime.Text = $"{CallStatus.ToString()} {timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
        }

        private void btnEndCall_Click(object sender, EventArgs e)
        {
            EndCall();
        }
    }
}
