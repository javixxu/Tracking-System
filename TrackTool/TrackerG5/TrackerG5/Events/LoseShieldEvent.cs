using System;

namespace TrackerG5
{
    [Serializable]

    class LoseShieldEvent : TrackerEvent
    {
        public LoseShieldEvent()
            : base()
        {
            typeEvent = "LoseShieldEvent";
        }
    }
}
