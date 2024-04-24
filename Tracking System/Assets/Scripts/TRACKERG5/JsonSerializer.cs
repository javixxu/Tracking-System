﻿using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;


namespace TrackerG5
{
    internal class JsonSerializer : ISerializer
    {
        public string Serialize(TrackerEvent e)

        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer jsonObj = new DataContractJsonSerializer(e.GetType());

            jsonObj.WriteObject(stream, e);
            stream.Seek(0, SeekOrigin.Begin);
            StreamReader streamReader = new StreamReader(stream);

            return "\n" + streamReader.ReadToEnd() + ",";
        }

        public void OpenFile(FileStream fs)
        {
            fs.Seek(0, SeekOrigin.Begin);
            if (fs.Length <= 0)
            {
                byte[] data = Encoding.UTF8.GetBytes("[");
                fs.Write(data, 0, data.Length);
            }
            else
            {
                // Mover el puntero de escritura al final del archivo
                fs.Seek(-1, SeekOrigin.End);
                // Truncar el archivo para eliminar el último carácter
                fs.SetLength(fs.Position);

                byte[] data = Encoding.UTF8.GetBytes(",");
                fs.Write(data, 0, data.Length);
            }
            fs.Seek(0, SeekOrigin.End);
        }

        public void EndFile(FileStream fs)
        {
            // Mover el puntero de escritura al final del archivo
            fs.Seek(-1, SeekOrigin.End);
            // Truncar el archivo para eliminar el último carácter
            fs.SetLength(fs.Position);

            byte[] data = Encoding.UTF8.GetBytes("]");
            fs.Write(data, 0, data.Length);
        }
    }
}
