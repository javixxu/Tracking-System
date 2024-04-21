using System;
using System.Runtime.Serialization.Json;


namespace TrackerG5
{
    internal class JsonSerializer : ISerializer
    {
        public JsonSerializer() {
            
                }
        public string Serialize(TrackerEvent e)
        {
            return "Funciona";
        }
    }
}
