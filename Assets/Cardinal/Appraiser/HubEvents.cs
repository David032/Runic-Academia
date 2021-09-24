using Runic.Entities;
using Runic.Items;
using Runic.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{    
    //@event.Name = "Player entered " + gameObject;
    //@event.Time = Time.realtimeSinceStartup.ToString();
    //@event.EventPriority = Cardinal.Priority.Low;
    public abstract class NPCEvent:EventData
    {
        public Entity NPC;
        public List<HexadCorrelation> Correlations;
    }
    public class NPCInteractionEvent : NPCEvent
    {

    }
    public class NPCTradeEvent : NPCEvent 
    {
        public InventoryChangeData ChangeData;
    }

    [System.Serializable]
    public class InventoryChangeData 
    {
        public InventoryChange Change;
        public Item Item;
        public int Amount;

        public InventoryChangeData(InventoryChange WhatSortOfChangeOccured, Item WhatChangedHands, int WhatWasItsValue) 
        {
            Change = WhatSortOfChangeOccured;
            Item = WhatChangedHands;
            Amount = WhatWasItsValue;
        }
    }
    public class BuildingEnteredEvent:EventData
    {
        public string BuildingName;
        public HexadCorrelation Correlation;
    }
    public class CollectibleFoundEvent : EventData 
    {
        public Item Item;
        public int SeriesNumber;
        public HexadCorrelation Correlation;
    }

    public class TaskTakenEvent : EventData
    {
        public Task Task;
        public HexadCorrelation HexadCorrelation;
    }
}
