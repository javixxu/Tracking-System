using System;

namespace TrackerG5
{
    internal class Login : TrackerEvent
    {
        public Login(string typeEvent, uint id, uint idUser, uint idSession, uint idLevel)
            : base(typeEvent, id, idUser, idSession, idLevel)
        {

        }
    }
}
