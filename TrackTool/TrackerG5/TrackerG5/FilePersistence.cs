using System;

namespace TrackerG5
{
    internal class FilePersistence : IPersistence
    {
        List<string> eventsQueue = new List<string>();
        Dictionary<ISerializer, List<string>> serializers;

        public FilePersistence()
        {

        }

        public void Send(TrackerEvent e)
        {
            //eventsQueue.Add();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }


    }
}
