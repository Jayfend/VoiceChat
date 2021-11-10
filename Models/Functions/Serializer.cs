using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public static class Serializer
    {
        public static byte[] Serialize(object obj) // phân mảnh
        {

            var objToString = JsonConvert.SerializeObject(obj);

            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, objToString);
            return stream.ToArray();
        }
        public static MessageModel Deserialize(byte[] data) // gộp mảnh
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            var stringObj = (string)formatter.Deserialize(stream);
            // make sure you use same type what you use chose during conversation

            return JsonConvert.DeserializeObject<MessageModel>(stringObj);

        }
    }
}
