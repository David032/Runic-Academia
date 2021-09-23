using Runic.Achievements;
using Runic.Items;
using Runic.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    public class TaskCompletedEvent : EventData
    {
        public Task Task;
        public HexadCorrelation Correlation;
    }
    public class AchievementCompletedEvent : EventData
    {
        public Achievement Achievement;
        public List<HexadCorrelation> Correlations;
    }

    public class InventoryChangeEvent : EventData 
    {
        public Item Item;
        public InventoryChange Change;
    }

    public class CurrencyChangeEvent : EventData 
    {
        public int Amount;
        public InventoryChange Change;
    }
}

