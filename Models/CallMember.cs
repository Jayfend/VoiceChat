using System.Net.Sockets;

namespace Models
{
    public class CallMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NetworkStream NetworkStream { get; set; }        
        public bool Ready { get; set; }
    }
}
