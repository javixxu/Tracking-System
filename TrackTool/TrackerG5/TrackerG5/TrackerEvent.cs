using System;
using System.Runtime.InteropServices.Marshalling;

namespace TrackerG5
{
    class TrackerEvent
    {
        string typeEvent;
        uint id {  get; set; }
        uint idUser { get; set; }
        uint idSession { get; set; }
        uint idLevel { get; set; }
        DateTime timestamp { get; }


        public TrackerEvent(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
        {
            this.typeEvent = typeEvent;
            this.id = id;
            this.idUser = idUser;
            this.idSession = idSession;
            this.idLevel = idLevel;
            timestamp = DateTime.UtcNow;
        }

        public string ToJson()
        {
            return "javichu juapo";
        }
    }
}
