using System;

namespace TrackerG5
{
    internal class StartGameEvent: TrackerEvent
    {
        public StartGameEvent(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
