using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    public class EventData : ScriptableObject
    {
        public string Name;
        public string Time;
        public List<BehaviourTypes> BehaviourIndicators = new List<BehaviourTypes>();
        public string Description;

        public void PopulateEventData(string EventName, string EventDescription, 
            List<BehaviourTypes> behaviours)
        {
            Name = EventName;
            Time = System.DateTime.Now.ToString();
            BehaviourIndicators = behaviours;
            Description = EventDescription;
        }
    }
}

