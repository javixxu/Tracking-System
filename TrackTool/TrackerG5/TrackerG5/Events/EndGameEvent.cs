using System;

namespace TrackerG5
{
    internal class EndGameEvent : TrackerEvent
    {
        public EndGameEvent(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
