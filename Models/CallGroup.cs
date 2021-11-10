using System.Collections.Generic;

namespace Models
{
    public class CallGroup
    {
        public int ID { get; set; }
        public List<CallMember> CallMembers { get; set; }
        public CallGroup()
        {
            CallMembers = new List<CallMember>();
        }
    }
}
