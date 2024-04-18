using System;

namespace TrackerG5
{
    internal class LoginEvent: TrackerEvent
    {
        public LoginEvent(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
