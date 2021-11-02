using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Group
    {
        public int ID { get; set; }
        List<Group> Groups { get; set; }
    }
}
