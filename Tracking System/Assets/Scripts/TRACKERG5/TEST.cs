using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TrackerG5.Tracker.Instance.Init(TrackerG5.Tracker.serializeType.Yaml, TrackerG5.Tracker.persistenceType.Disc);
        TrackerG5.Tracker.Instance.AddEvent(TrackerG5.Tracker.eventType.LoseShield);
        TrackerG5.Tracker.Instance.AddEvent(TrackerG5.Tracker.eventType.Death);
        TrackerG5.Tracker.Instance.End();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
