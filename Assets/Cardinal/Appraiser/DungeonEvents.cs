using Runic.Entities;
using Runic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    //@event.Name = "Player entered " + gameObject;
    //@event.Time = Time.realtimeSinceStartup.ToString();
    //@event.EventPriority = Cardinal.Priority.Low;

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
    public class PlayerDeathEvent : EventData 
    {
        public GameObject RoomOfDeath;
        public Entity Slayer;
    }
    public class CompletedDungeonEvent : EventData
    {
        public HexadCorrelation Correlation;
    }
}

