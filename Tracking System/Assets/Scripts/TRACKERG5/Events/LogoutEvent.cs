using System;

namespace TrackerG5
{
    internal class LogoutEvent: TrackerEvent
    {
        public LogoutEvent()
            : base()
        {
            typeEvent = "LogoutEvent";
        }
    }
}
