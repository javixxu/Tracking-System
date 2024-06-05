using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrackerG5
{
    internal class FilePersistence : IPersistence
    {
        List<TrackerEvent> eventsQueue = new List<TrackerEvent>();
        ISerializer mySerializer;
        FileStream fs;
        uint maxSizeQueue;

        public FilePersistence(ISerializer serializer, string route, uint maxSizeQueue)
        {
            this.maxSizeQueue = maxSizeQueue;
            mySerializer = serializer;

            createFileStream(route);
        }

        private bool createFileStream(string route)
        {
            try
            {
                fs = new FileStream(route, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                //Esto habría que quitarlo ya que el archivo tambien lo tiene que abrir FilePersistencie
                mySerializer.OpenFile(fs);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error en la creación del archivo", ex);
            }

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
            foreach (var item in eventsQueue)
            {
                byte[] data = Encoding.UTF8.GetBytes(mySerializer.Serialize(item));
                fs.Write(data, 0, data.Length);
            }
        }
        void closeFile()
        {
            mySerializer.EndFile(fs);
            fs.Close();
        }

        public void EndSession()
        {
            Flush();
            closeFile();
        }
    }
}
