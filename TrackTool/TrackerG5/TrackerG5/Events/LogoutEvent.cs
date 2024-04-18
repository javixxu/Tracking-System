using System;

namespace TrackerG5
{
    internal class Logout : TrackerEvent
    {
        public Logout(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
