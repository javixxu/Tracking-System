using System;
using System.IO;

namespace TrackerG5
{
    internal class FilePersistence : IPersistence
    {
        List<TrackerEvent> eventsQueue = new List<TrackerEvent>();
        ISerializer mySerializer;
        StreamWriter writer;
        public FilePersistence(ISerializer serializer,string route )
        {
            writer = new StreamWriter(route);
            mySerializer = serializer;
        }

        public void Send(TrackerEvent e)
        {
            eventsQueue.Add(e);
        }

        public void Flush()
        {
            foreach (var item in eventsQueue){
                 writer.WriteLine(mySerializer.Serialize(item));
            }
        }
        void closeFile()
        {
            writer.Close();
        }
        
        ~FilePersistence()
        {
            closeFile();
        }
    }
}
