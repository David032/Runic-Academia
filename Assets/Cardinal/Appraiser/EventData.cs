using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    public class EventData : ScriptableObject
    {
        public string Name;
        public string Time;
        public HexadCorrelation Correleation;
        public Priority EventPriority = Priority.Medium;
    }
    public class MultiEventData : EventData 
    {
        public HexadCorrelation secondaryCorrelation;
    }

    [System.Serializable]
    public class HexadCorrelation 
    {
        public HexadTypes Type;
        public int Amount;

        public HexadCorrelation(HexadTypes CategoryType, int AmountToChangeBy) 
        {
            Type = CategoryType;
            Amount = AmountToChangeBy;
        }
    }
}

