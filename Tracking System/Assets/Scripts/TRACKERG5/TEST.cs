using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TrackerG5.Tracker.Instance.Init(TrackerG5.Tracker.serializeType.Json, TrackerG5.Tracker.persistenceType.Disc);
        //TrackerG5.Tracker.Instance.AddEvent(new TrackerG5.LoginEvent());
        //TrackerG5.Tracker.Instance.AddEvent(new TrackerG5.DeathEvent());
        TrackerG5.Tracker.Instance.End();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
