using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Group
    {
        public int ID { get; set; }
        public List<Socket> SocketGroupList { get; set; }
        public Group(int ID_)
        {
            ID = ID_;
            SocketGroupList = new List<Socket>();
        }
    }
}
