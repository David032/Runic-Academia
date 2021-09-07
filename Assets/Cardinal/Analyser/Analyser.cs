using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal.Appraiser;
using System.Linq;

namespace Cardinal.Analyser
{
    public class Analyser : CardinalSingleton<Analyser> 
    {
        //These will not be public in final
        public float PhilanthropistValue;
        public float SocialiserValue;
        public float FreeSpiritValue;
        public float AchieverValue;
        public float DisruptorValue;
        public float PlayerValue;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        #region Mk1 Analyser
        [Header("Inbound Event Buffer")]
        public List<EventData> Events;
        [Header("Max Events Before Pass")]
        public int EventLimit = 8;
        [Header("Time Between Scheduled Passes")]
        public float PassTime = 30f;

        public void RegisterEvent(EventData data) 
        {
            Events.Add(data);
            if (Events.Count > EventLimit)
            {
                print("Event limit reached! Commence analysis");
                BasicAnalysis();
            }
        }

        /// <summary>
        /// Counts the behaviour indicators in each of the events buffer 
        /// and prints out the entries
        /// </summary>
        void BasicAnalysis() 
        {
            //Maybe swap for a sortedictionary
            SortedDictionary<string, int> BehaviourCount = 
                new SortedDictionary<string, int>();
            foreach (EventData eventEntry in Events)
            {
                foreach (BehaviourTypes indicitiveBehaviour 
                    in eventEntry.BehaviourIndicators)
                {
                    if (!BehaviourCount.ContainsKey(indicitiveBehaviour.
                        ToString()))
                    {
                        BehaviourCount.Add(indicitiveBehaviour.ToString(), 1);
                    }
                    else
                    {
                        BehaviourCount[indicitiveBehaviour.ToString()] += 1;
                    }
                }
            }


            print("Out of " + Events.Count + " events:");
            foreach (var item in BehaviourCount)
            {
                print(item.Value + " events exhibited " + item.Key 
                    + " behaviours");
            }
        }
        #endregion
    }
}
