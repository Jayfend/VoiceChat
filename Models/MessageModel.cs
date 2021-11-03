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
        public string Name { get; set; }
        public string Message { get; set; }
        //Day la property 
        public MessageModel(int ID_, int group_ID_, string audio_ID_, string name_)
        {
            ID = ID_;
            Group_ID = group_ID_;
            Audio_ID = audio_ID_;
            Name = name_;
        }
       
    }
}
