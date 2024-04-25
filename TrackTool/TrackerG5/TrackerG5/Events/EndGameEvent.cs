using System;

namespace TrackerG5
{
    [Serializable]
    class EndGameEvent : TrackerEvent
    {
        public EndGameEvent()
            : base()
        {
            typeEvent = "EndGameEvent";
        }
    }
}
