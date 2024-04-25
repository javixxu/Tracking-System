﻿using System;
using System.Collections.Generic;

namespace TrackerG5
{
    [Serializable]
    internal class TrackerEvent
    { 
        protected string typeEvent;
        
        string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        string idUser;
        public string IdUser
        {
            get { return idUser; }
            set { idUser = value; }
        }

        string idSession;
        public string IdSession
        {
            get { return idSession; }
            set { idSession = value; }
        }

        uint idLevel;
        public uint IdLevel
        {
            get { return idLevel; }
            set { idLevel = value; }
        }

        DateTime timestamp;
        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public void SetParamns(Dictionary<string, string> paramns)
        {
           
        }


        public TrackerEvent()
        {
            //this.typeEvent = typeEvent;
            //this.id = id;
            //this.idUser = idUser;
            //this.idSession = idSession;
            //this.idLevel = idLevel;
            //timestamp = DateTime.UtcNow;
        }

        public string ToJson()
        {
            return "";
        }
    }
}
