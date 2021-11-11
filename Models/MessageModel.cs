using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MessageModel
    {
        //Day la property 
        public int ID { get; set; }
        public int Group_ID { get; set; }
        public string Audio_ID { get; set; }
        public byte[] VoiceData { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public MessageType MessageType { get; set; }
        //Day la property 
        public MessageModel(int ID_, int group_ID_, string audio_ID_, string name_)
        {
            ID = ID_;
            Group_ID = group_ID_;
            Audio_ID = audio_ID_;
            Name = name_;
            VoiceData = null;
            MessageType = MessageType.Text;
        }
        public MessageModel()
        {
            MessageType = MessageType.Text;
        }
    }

    public enum MessageType
    {
        Text = 0,
        Call = 1,
        CallAccepted = 2
    }

    public enum CallStatus
    {
        Pending = 0,
        Calling = 1,
        Ended = 2
    }
}
