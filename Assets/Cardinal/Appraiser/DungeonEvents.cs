using Runic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    public class DungeonEvents : MonoBehaviour
    {
    }

    public class RoomEnteredEvent:EventData
    {
        public RoomType RoomType = RoomType.MainRoom;
        public bool IsFirstEntry = false;
        public HexadCorrelation Correleation;
    }

    public class NodeInteractedWith : EventData 
    {
        public NodeType NodeType = NodeType.Chest;
        public List<Item> items;
        public RoomType RoomType = RoomType.MainRoom;
        public HexadCorrelation Correlation;
    }
}

