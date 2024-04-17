using System;

namespace TrackerG5
{
    internal class StartGame : TrackerEvent
    {
        public StartGame(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
