using System;

namespace TrackerG5
{
    internal class LoseShield : TrackerEvent
    {
        public LoseShield(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
