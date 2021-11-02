using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal.Appraiser;
using System.Linq;
using Cardinal.Generative.Dungeon;
using Cardinal.Adjustor;

namespace Cardinal.Analyser
{
    public class Analyser : CardinalSingleton<Analyser> 
    {
        [Header("Profiled Values - Hexad")]//These shouldn't be public in final
        public float PhilanthropistValue;
        public float SocialiserValue;
        public float FreeSpiritValue;
        public float AchieverValue;
        public float DisruptorValue;
        public float PlayerValue;
        [Header("Profiled Values - NonHexad")]
        public float KillDeathRatio;
        public float DungeonProgression;
        public PlayerPerformance Performance = PlayerPerformance.Average;
        public int DungeonsCompletedCount = 0;
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
            InvokeRepeating("ScheduledAnalysis", PassTime, PassTime);
        }

        private void Update()
        {
            
        }

        #region Core Functionality

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
                    ImmediateAnalysis();
                    break;
                default:
                    break;
            }
        }

        public void ReflectiveAnalysis() 
        {
            AnalyseLowPriority();
            ProfileCompletionEfficency();
            if (DungeonsCompletedCount != DungeonsCompleted())
            {
                //[ADJ]Call the adjustor to make the change
                DungeonsCompletedCount = DungeonsCompleted();
            }

        }
        public void ScheduledAnalysis() 
        {
            AnalyseMediumPriority();
            if (KillDeathRatio != CalculateKillDeathRatio())
            {
                if (KillDeathRatio > CalculateKillDeathRatio())
                {
                    //[ADJ] player deaths increasing
                }
                if (KillDeathRatio < CalculateKillDeathRatio())
                {
                    //[ADJ] Enemy kills increasing
                }
                KillDeathRatio = CalculateKillDeathRatio();
            }
            DungeonProgression = CalculateProgressThroughDungeon();
            if (DungeonProgression > 0.5)
            {
                print("Player is exploring the dungeon?");
                Adjustor.Adjustor.Instance.Message(ResponseSubject.Player, ResponseValue.DungeonProgression, ResponseAction.Increase);
            }

            ProfileRoomRoutingNavigationBehaviour();
            ProfileEnemyKills();
        }
        public void ImmediateAnalysis() 
        {
            AnalyseHighPriority();
            DeathAssessment();
        }
        #endregion

        #region Event Queue Analysis
        public void AnalyseLowPriority() 
        {
            if (LowPriorityEvents.Count == 0)
            {
                return;
            }
            UpdateHexadModel(LowPriorityEvents);
        }
        public void AnalyseMediumPriority() 
        {
            if (MediumPriorityEvents.Count == 0)
            {
                return;
            }
            UpdateHexadModel(MediumPriorityEvents);
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
        /// <summary>
        /// Update player hexad model
        /// </summary>
        /// <param name="eventsToWorkFrom"></param>
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
            //[ADJ]Indicate what the player's primary player type is
        }
        /// <summary>
        /// Identify if the player is sticking to the main path
        /// </summary>
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
        /// <summary>
        /// Identify if the player isn't using the task system
        /// </summary>
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
        /// <summary>
        /// Identify if the player is only fighting the bosses
        /// </summary>
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
                //[ADJ] player is only fighting the boss - only fires after they've killed a boss
            }
        }
        /// <summary>
        /// Identify if the player is struggling
        /// </summary>
        void DeathAssessment() 
        {
            int deaths = 0;
            DungeonEnteredEvent entryData = null;
            List<EventData> Events = GetAllEvents();
            foreach (EventData item in Events)
            {
                if (item is PlayerDeathEvent)
                {
                    deaths++;
                }
                if (item is DungeonEnteredEvent @event)
                {
                    entryData = @event;
                }
            }
            int AllowedDeaths = AllowableDeaths(entryData);
            if (deaths <= AllowedDeaths)
            {
                //Things are fine
            }
            if (deaths > AllowedDeaths)
            {
                //[ADJ]Request adjustment here
                Performance--;
            }
        }

        /// <summary>
        /// Identify dungeons completed
        /// </summary>
        /// <returns>Number of dungeons completed</returns>
        int DungeonsCompleted() 
        {
            int NumberCompleted = 0;
            List<EventData> Events = GetAllEvents();
            foreach (EventData item in Events)
            {
                if (item is CompletedDungeonEvent @event)
                {
                    NumberCompleted++;
                }
            }
            return NumberCompleted;
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

        //Calculate Permitted Number of Deaths
        int AllowableDeaths(DungeonEnteredEvent dungeonData) 
        {
            int AllowedDeaths = 0;
            if (dungeonData.RequiresBoss)
            {
                AllowedDeaths++;
            }
            if (dungeonData.Size != Generative.SizeOfDungeon.Small)
            {
                AllowedDeaths++;
            }
            if (dungeonData.EnemyAmount >= Generative.ResourceAvailability.Abundant)
            {
                AllowedDeaths++;
            }
            return AllowedDeaths;
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
