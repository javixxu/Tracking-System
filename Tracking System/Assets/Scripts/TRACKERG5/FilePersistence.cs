using System;
using System.Collections.Generic;
using System.IO;

namespace TrackerG5
{
    internal class FilePersistence : IPersistence
    {
        List<TrackerEvent> eventsQueue = new List<TrackerEvent>();
        ISerializer mySerializer;
        StreamWriter writer;
        uint maxSizeQueue;
        public FilePersistence(ISerializer serializer,string route, uint maxSizeQueue)
        {
            this.maxSizeQueue = maxSizeQueue;
            writer = new StreamWriter(route);
            mySerializer = serializer;
        }

        public void Send(TrackerEvent e)
        {
            eventsQueue.Add(e);

            if (eventsQueue.Count == maxSizeQueue)
            {
                Flush();
                eventsQueue.Clear();
            }

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
        
        public void EndSession()
        {
            Flush();
            closeFile();
        }
    }
}
