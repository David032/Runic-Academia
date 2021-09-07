using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal.Analyser;

namespace Cardinal.Appraiser
{
    public class BaseEvent : MonoBehaviour
    {
        public string Name;
        public List<BehaviourTypes> BehaviourIndicators = new List<BehaviourTypes>();
        public string Description;

        public void LogEvent()
        {
            EventData GeneratedEvent = (EventData)EventData.CreateInstance("EventData");
            GeneratedEvent.PopulateEventData(Name, Description, BehaviourIndicators);
            Analyser.Analyser.Instance.RegisterEvent(GeneratedEvent);
        }
    }
}


