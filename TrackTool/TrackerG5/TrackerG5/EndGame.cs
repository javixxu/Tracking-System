using System;

namespace TrackerG5
{
    internal class EndGame : TrackerEvent
    {
        public EndGame(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
