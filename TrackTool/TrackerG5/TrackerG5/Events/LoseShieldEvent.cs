using System;

namespace TrackerG5
{
    internal class LoseShieldEvent: TrackerEvent
    {
        public LoseShieldEvent(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
