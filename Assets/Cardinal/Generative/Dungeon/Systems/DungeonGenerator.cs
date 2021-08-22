using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Dungeon
{
    public class DungeonGenerator : Generator
    {
        [Header("Variables")]
        //Size controls the max number of rooms generated
        public SizeOfDungeon DungeonSize = SizeOfDungeon.Small;
        //Type determines how it'll generate
        public TypeOfDungeon DungeonType = TypeOfDungeon.Branching;
        [Header("Data Objects")]
        public RoomList RoomList;
        [Header("Generated Data")]
        public List<GameObject> GeneratedRooms;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public IEnumerator GenerateRoom
            (List<RoomFlags> requiredFlags, Vector3 location) 
        {
            List<GameObject> validrooms = new List<GameObject>();

            //Find Valid Rooms
            foreach (GameObject item in RoomList.RoomsToUse)
            {
                List<RoomFlags> roomFlags = item.GetComponent<Room>().RoomFlags;
                roomFlags.Sort();
                requiredFlags.Sort();
                if (roomFlags == requiredFlags)
                {
                    validrooms.Add(item);
                }
            }
            //Select Room to Spawn
            int randomSelection = Random.Range(0, validrooms.Count);
            GameObject roomToSpawn = Instantiate(validrooms[randomSelection]);
            roomToSpawn.transform.position = location;
            GeneratedRooms.Add(roomToSpawn);
            yield return new WaitForEndOfFrame();
        }
    }    
}

