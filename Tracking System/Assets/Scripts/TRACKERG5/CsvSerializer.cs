using System;
using System.IO;
using System.Reflection;
using System.Text;


namespace TrackerG5
{
    internal class CsvSerializer : ISerializer
    {

        bool d = false;

        public string Serialize(TrackerEvent e)
        {

            StringBuilder csvBuilder = new StringBuilder();

            PropertyInfo[] properties = e.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                csvBuilder.AppendLine($"{property.Name},{property.GetValue(e)}");
            }

            return csvBuilder.ToString();
        }
        public void OpenFile(FileStream fs)
        {
            throw new NotImplementedException();
        }

        public void EndFile(FileStream fs)
        {
            throw new NotImplementedException();
        }
    }
}
