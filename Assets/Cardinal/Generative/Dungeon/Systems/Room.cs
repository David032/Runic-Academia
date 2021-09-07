using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Dungeon
{
    public class Room : MonoBehaviour
    {
        public List<Doorway> doorways;
        public List<RoomFlags> RoomFlags;
        public bool isDone = false;
        public bool isBroken = false;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
