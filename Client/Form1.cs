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
                     memberinfo = Deserialize(data);
                    if (!string.IsNullOrEmpty(memberinfo.Message))
                    {
                        string s = memberinfo.Name + ": " + memberinfo.Message;
                        AddMessage(s);
                    }
                    if (!string.IsNullOrEmpty(memberinfo.Audio_ID))
                    {
                        AddVoice(memberinfo);

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
                Client.Send(Serialize(member));
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
            var stringObj = (string)formatter.Deserialize(stream);
            // make sure you use same type what you use chose during conversation

            return JsonConvert.DeserializeObject<MessageModel>(stringObj);

        }
        void AddVoice(MessageModel message)
        {            
            lsvMessage.Items.Add(new ListViewItem()
            {
                Text = $"[>Voice: {message.Name}<]",
                Tag = message.Audio_ID,
                ImageIndex = 0,                
                BackColor = Color.LimeGreen,
                ForeColor = Color.White
            });
        }
        void AddMessage(String s)
        {
            lsvMessage.Items.Add(new ListViewItem()
            {
                Text = s,
                ImageIndex = 1
            }); ;
            txtMessage.Clear();
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

            Client.Send(Serialize(member));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
                  Send();
                AddMessage(txtName.Text+": "+txtMessage.Text);

            
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
                AddVoice(member);
                Client.Send(Serialize(member));
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
            if(lsvMessage.SelectedItems.Count > 0)
            {
                string voiceId = lsvMessage.SelectedItems[0].Tag.ToString();
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
            listenThread.Abort();
        }
    }
}
