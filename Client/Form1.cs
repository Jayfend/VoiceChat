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
            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();
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
                    string s = memberinfo.Name + " : " + memberinfo.Message;
                    AddMessage(s);
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
        void AddMessage(String s)
        {
            lsvMessage.Items.Add(new ListViewItem() { Text = s });
            txtMessage.Clear();
        }
        void Close() //đóng kết nối
        {
            Client.Close();

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty || txtID.Text == string.Empty || txtGroupID.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            CheckForIllegalCrossThreadCalls = false;
            Connect();
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

                Client.Send(Serialize(member));
            }
            else
            {
                MessageBox.Show("Voice audio cancelled");
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
