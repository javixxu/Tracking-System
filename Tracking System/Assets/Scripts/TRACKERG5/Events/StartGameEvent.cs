using System;

namespace TrackerG5
{
    [Serializable]

    class StartGameEvent : TrackerEvent
    {
        
        public StartGameEvent()
            : base()
        {
            typeEvent = "StartGameEvent";
        }
    }
}
