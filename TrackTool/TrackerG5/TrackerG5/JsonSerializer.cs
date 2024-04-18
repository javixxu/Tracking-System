using System;
using System.Text.Json;

namespace TrackerG5
{
    internal class JsonSerializer : ISerializer
    {
        string jsonFileName = "";
        public JsonSerializer() { }
        public string Serialize(TrackerEvent e)
        {
            return System.Text.Json.JsonSerializer.Serialize(e);
        }

    }
}
