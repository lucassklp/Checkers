using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Socket
{
    class Serializer
    {
        public static byte[] Serialize<T>(T data) where T : class
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public static T Deserialize<T>(byte[] param) where T : class
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                IFormatter binary = new BinaryFormatter();
                return binary.Deserialize(ms) as T;
            }
        }
    }
}
