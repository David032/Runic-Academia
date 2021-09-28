using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal.Appraiser;
using System.Linq;
using Cardinal.Generative.Dungeon;

namespace Cardinal.Analyser
{
    public class Analyser : CardinalSingleton<Analyser> 
    {
        //These will not be public in final
        [Header("Profiled Values - Hexad")]
        public float PhilanthropistValue;
        public float SocialiserValue;
        public float FreeSpiritValue;
        public float AchieverValue;
        public float DisruptorValue;
        public float PlayerValue;
        [Header("Profiled Values - NonHexad")]
        public float KillDeathRation;
        public float DungeonProgression;
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

        #region Analysis Queues
        public void AnalyseLowPriority() 
        {
            if (LowPriorityEvents.Count == 0)
            {
                return;
            }
            UpdateHexadModel(LowPriorityEvents);
            ProfileCompletionEfficency();
        }

        public void AnalyseMediumPriority() 
        {
            if (MediumPriorityEvents.Count == 0)
            {
                return;
            }
            UpdateHexadModel(MediumPriorityEvents);
            KillDeathRation = CalculateKillDeathRatio();
            DungeonProgression = CalculateProgressThroughDungeon();
            ProfileRoomRoutingNavigationBehaviour();
            ProfileEnemyKills();
        }

        public void AnalyseHighPriority()
        {
            if (HighPriorityEvents.Count == 0)
            {
                return;
            }
            UpdateHexadModel(HighPriorityEvents);
        }
        #endregion

        #region Analysis Functions
        //Update player hexad model
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

        }
        //Identify if the player is sticking to the main path
        void ProfileRoomRoutingNavigationBehaviour() 
        {
            //If Player is only entering main path rooms -Free Spirit
            //Room entered events are on the medium priority queue
            int NonMainRooms = 0;
            foreach (EventData item in MediumPriorityEvents)
            {
                if (item is RoomEnteredEvent @event)
                {
                    if (@event.RoomType != RoomType.MainRoom)
                    {
                        NonMainRooms++;
                    }
                }
            }
            if (NonMainRooms != 0)
            {
                ApplyCorrelation(new HexadCorrelation(HexadTypes.FreeSpirits,-300));
            }
        }
        //Identify if the player isn't using the task system
        void ProfileCompletionEfficency() 
        {
            Runic.Tasks.TaskManager taskManager = Runic.Tasks.TaskManager.Instance;
            //If Player has no tasks active and none completed at dungeon completion, +Free Spirit(Major)
            if (taskManager.ActiveEndeavours.Count == 0 && taskManager.ActiveJobs.Count == 0 
                && taskManager.ActiveQuest == null && taskManager.CompletedTasks.Count == 0 )
            {
                ApplyCorrelation(new HexadCorrelation(HexadTypes.FreeSpirits, 300));
                ApplyCorrelation(new HexadCorrelation(HexadTypes.Disruptors, 100));
            }
        }
        //Identify if the player is only fighting the bosses
        void ProfileEnemyKills() 
        {
            int normalKills = 0;
            int bossKills = 0;
            foreach (EventData item in MediumPriorityEvents)
            {
                if (item is EnemyKilledEvent @event)
                {
                    switch (@event.EnemyCategory)
                    {
                        case Runic.EnemyCategory.Add:
                            normalKills++;
                            break;
                        case Runic.EnemyCategory.Leader:
                            normalKills++;
                            break;
                        case Runic.EnemyCategory.Boss:
                            bossKills++;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (normalKills == 0 && bossKills != 0)
            {
                ApplyCorrelation(new HexadCorrelation(HexadTypes.Disruptors, 300));
            }
        }
        #endregion

        #region Calculation Functions
        //Calculate K/D Ratio
        float CalculateKillDeathRatio() 
        {
            int Kills = 0;
            int Deaths = 0;

            List<EventData> Events = GetAllEvents();

            foreach (EventData item in Events)
            {
                if (item is PlayerDeathEvent)
                {
                    Deaths++;
                }
                if (item is EnemyKilledEvent)
                {
                    Kills++;
                }
            }
            return Kills / Deaths;
        }
        //Calculate player progress through dungeon
        float CalculateProgressThroughDungeon() 
        {
            List<EventData> Events = GetAllEvents();
            int ProgressThroughDungeon = 0;
            int TotalRooms = 0;
            DungeonGenerator Dungeon = (DungeonGenerator)DungeonGenerator.Instance;
            foreach (GameObject item in Dungeon.GeneratedRooms)
            {
                if (item.activeSelf)
                {
                    TotalRooms++;
                }
            }
            foreach (EventData item in Events)
            {
                if (item is RoomEnteredEvent @event && @event.IsFirstEntry == true)
                {
                    ProgressThroughDungeon++;
                }
            }
            return ProgressThroughDungeon / TotalRooms;
        }
        #endregion

        #region Misc
        List<EventData> GetAllEvents() 
        {
            List<EventData> MasterList = new List<EventData>();
            MasterList.AddRange(LowPriorityEvents.ToList());
            MasterList.AddRange(MediumPriorityEvents.ToList());
            MasterList.AddRange(HighPriorityEvents.ToList());
            return MasterList;
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

        #endregion
    }
}
