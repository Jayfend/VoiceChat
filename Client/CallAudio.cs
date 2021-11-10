using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class CallAudio : Form
    {
        private int Port = 7749;
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupId { get; set; }
        public TcpClient Socket { get; set; }
        public CallAudio(int id, string name, string groupId)
        {
            InitializeComponent();            
        }
    }
}
