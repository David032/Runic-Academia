using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        int RoomsGenerated = 0;
        GameObject SpawnRoom;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GenerateStartingRoom() 
        {
            List<GameObject> validrooms = new List<GameObject>();
            Vector3 location = Vector3.zero;

            //Find Valid Rooms
            foreach (GameObject item in RoomList.RoomsToUse)
            {
                if (item.GetComponent<Room>().RoomFlags.Contains
                    (RoomFlags.StartingRoom))
                {
                    validrooms.Add(item);
                }
            }
            //Select Room to Spawn
            int randomSelection = Random.Range(0, validrooms.Count);
            GameObject roomToSpawn = Instantiate(validrooms[randomSelection]);
            roomToSpawn.transform.position = location;
            GeneratedRooms.Add(roomToSpawn);
            SpawnRoom = roomToSpawn;
        }

        public Doorway GetMainPathStart() 
        {
            Room startingRoom = SpawnRoom.GetComponent<Room>();
            int randomDoorSelection = Random.Range(0, startingRoom.doorways.Count);
            return startingRoom.doorways[randomDoorSelection];
        }

        public Doorway GetRandomDoorway(Room room) 
        {
            int randomDoorSelection = Random.Range(0, room.doorways.Count);
            return room.doorways[randomDoorSelection];
        }
        void GenerateMainPath() 
        {
            GameObject currentMainNode;
            Doorway currentDoor;

            GenerateStartingRoom();
            currentMainNode = 
                GenerateAndReturnRoom(GetMainPathStart().GetNextRoomSpawnLocation()); //Spawn room off of starting room
            for (int i = 0; i < (int)DungeonSize / 2; i++)
            {
                currentDoor = GetRandomDoorway(currentMainNode.GetComponent<Room>());
                currentMainNode = GenerateAndReturnRoom
                    (currentDoor.GetNextRoomSpawnLocation());
            }
        }

        public void GenerateRoom(List<RoomFlags> roomFlags, Vector3 position) 
        {
            List<GameObject> validrooms = new List<GameObject>();

            //Find Valid Rooms
            foreach (GameObject item in RoomList.RoomsToUse)
            {
                if (item.GetComponent<Room>().RoomFlags == roomFlags)
                {
                    validrooms.Add(item);
                }
            }
            //Select Room to Spawn
            int randomSelection = Random.Range(0, validrooms.Count);
            GameObject roomToSpawn = Instantiate(validrooms[randomSelection]);
            roomToSpawn.transform.position = position;
            GeneratedRooms.Add(roomToSpawn);
        }

        public void GenerateRoom(Vector3 position)
        {
            List<GameObject> validrooms = RoomList.RoomsToUse;

            //Select Room to Spawn
            int randomSelection = Random.Range(0, validrooms.Count);
            GameObject roomToSpawn = Instantiate(validrooms[randomSelection]);
            roomToSpawn.transform.position = position;
            GeneratedRooms.Add(roomToSpawn);
        }

        public GameObject GenerateAndReturnRoom(Vector3 position)
        {
            List<GameObject> validrooms = RoomList.RoomsToUse;

            //Select Room to Spawn
            int randomSelection = Random.Range(0, validrooms.Count);
            GameObject roomToSpawn = Instantiate(validrooms[randomSelection]);
            roomToSpawn.transform.position = position;
            GeneratedRooms.Add(roomToSpawn);
            return roomToSpawn;
        }

    }
}

