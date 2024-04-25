using System;

namespace TrackerG5
{
    [Serializable]
    class DeathEvent : TrackerEvent
    {
        public DeathEvent()
            : base()
        {
            typeEvent = "DeathEvent";
        }
    }
}
