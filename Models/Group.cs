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
        public List<Member> SocketGroupList { get; set; }
        public Group()
        {
            SocketGroupList = new List<Member>();
        }
    }
}
