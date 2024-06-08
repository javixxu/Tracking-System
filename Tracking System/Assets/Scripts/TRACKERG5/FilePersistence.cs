using DG.Tweening.Plugins.Core.PathCore;
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
        StreamWriter writer;
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
                 writer = new StreamWriter(route);

                //Esto habría que quitarlo ya que el archivo tambien lo tiene que abrir FilePersistencie
                writer.Write(mySerializer.OpenFile());
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
                writer.Write(mySerializer.Serialize(item));
            }
        }
        void closeFile()
        {
            writer.Write(mySerializer.EndFile());
            writer.Close();
        }

        public void EndSession()
        {
            Flush();
            closeFile();
        }
    }
}
