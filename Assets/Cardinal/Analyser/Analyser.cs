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
        [Header("Profiled Values")]
        public float PhilanthropistValue;
        public float SocialiserValue;
        public float FreeSpiritValue;
        public float AchieverValue;
        public float DisruptorValue;
        public float PlayerValue;

        [Header("Inbound Events Buffer")]
        public List<EventData> LowPriorityEvents;
        public List<EventData> MediumPriorityEvents;
        public List<EventData> HighPriorityEvents;

        [Header("Max Events Before Pass")]
        public int EventLimit = 8;
        [Header("Time Between Scheduled Passes")]
        public float PassTime = 30f;

        private void Start()
        {
            InvokeRepeating("AnalyseMediumPriority", PassTime, PassTime);
        }

        private void Update()
        {
            
        }



        public void RegisterEvent(EventData data) 
        {
            switch (data.EventPriority)
            {
                case Priority.Low:
                    LowPriorityEvents.Add(data);
                    break;
                case Priority.Medium:
                    MediumPriorityEvents.Add(data);
                    break;
                case Priority.High:
                    HighPriorityEvents.Add(data);
                    AnalyseHighPriority();
                    break;
                default:
                    break;
            }
        }

        public void AnalyseLowPriority() 
        {
            if (LowPriorityEvents.Count == 0)
            {
                return;
            }
            UpdateHexadModel(LowPriorityEvents);
            //More Processing?
            //Clear the buffer?
        }

        public void AnalyseMediumPriority() 
        {
            if (MediumPriorityEvents.Count == 0)
            {
                return;
            }
            UpdateHexadModel(MediumPriorityEvents);
            //More Processing?
            //Clear the buffer?
        }

        public void AnalyseHighPriority()
        {
            if (HighPriorityEvents.Count == 0)
            {
                return;
            }
            UpdateHexadModel(HighPriorityEvents);
            //More Processing?
            //Clear the buffer?
        }

        void UpdateHexadModel(List<EventData> eventsToWorkFrom) 
        {
            foreach (EventData item in eventsToWorkFrom)
            {
                if (item.Correleation != null)
                {
                    ApplyCorrelation(item.Correleation);
                }
                if (item is MultiEventData eventData)
                {
                    if (eventData.secondaryCorrelation != null)
                    {
                        ApplyCorrelation(eventData.secondaryCorrelation);
                    }
                }
            }

            void ApplyCorrelation(HexadCorrelation item)
            {
                switch (item.Type)
                {
                    case HexadTypes.Philanthropists:
                        PhilanthropistValue += item.Amount;
                        break;
                    case HexadTypes.Socialisers:
                        SocialiserValue += item.Amount;
                        break;
                    case HexadTypes.FreeSpirits:
                        FreeSpiritValue += item.Amount;
                        break;
                    case HexadTypes.Achievers:
                        AchieverValue += item.Amount;
                        break;
                    case HexadTypes.Players:
                        PlayerValue += item.Amount;
                        break;
                    case HexadTypes.Disruptors:
                        DisruptorValue += item.Amount;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
