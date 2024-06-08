using System.IO;
using System.Runtime.Serialization.Json;

namespace TrackerG5
{
    internal class JsonSerializer : ISerializer
    {

        private bool firstEvent = true;

        public string Serialize(TrackerEvent e)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer jsonObj = new DataContractJsonSerializer(e.GetType());

            jsonObj.WriteObject(stream, e);
            stream.Seek(0, SeekOrigin.Begin);
            StreamReader streamReader = new StreamReader(stream);

            if (!firstEvent)
                return "," + "\n" + streamReader.ReadToEnd();
            else
            {
                firstEvent = false;
                return "\n" + streamReader.ReadToEnd();
            }
         
        }

        public string OpenFile()
        {
            //fs.Seek(0, SeekOrigin.Begin);
            //if (fs.Length <= 0)
            //{
            //    fs.WriteByte((byte)'[');
            //}
            //else
            //{
            //    // Mover el puntero de escritura al final del archivo
            //    fs.Seek(-1, SeekOrigin.End);
            //    // Truncar el archivo para eliminar el último carácter
            //    fs.SetLength(fs.Position);

            //    fs.WriteByte((byte)',');
            //}
            //fs.Seek(0, SeekOrigin.End);
            return "[";
        }

        public string EndFile()
        {
            //// Mover el puntero de escritura al final del archivo
            //fs.Seek(-1, SeekOrigin.End);
            //// Truncar el archivo para eliminar el último carácter
            //fs.SetLength(fs.Position);

            //fs.WriteByte((byte)']');
            return "]";
        }
    }
}
