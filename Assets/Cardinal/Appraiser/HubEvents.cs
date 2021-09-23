using Runic.Entities;
using Runic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    public abstract class NPCEvent:EventData
    {
        public Entity NPC;
        public List<HexadCorrelation> Correlations;
    }
    public class NPCInteractionEvent : NPCEvent
    {

    }
    public class NPCTradeEvent : EventData 
    {
        public InventoryChangeData ChangeData;
    }

    [System.Serializable]
    public class InventoryChangeData 
    {
        public InventoryChange Change;
        public Item Item;
        public int Amount;
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
}
