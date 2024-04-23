using System;
using System.IO;
using System.Runtime.Serialization.Json;


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

            return streamReader.ReadToEnd();
        }
    }
}
