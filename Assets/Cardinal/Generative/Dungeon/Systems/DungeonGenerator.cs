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
        public RoomList StarterRooms;
        [Header("Generated Data")]
        public List<GameObject> GeneratedRooms;
        int RoomsGenerated = 0;
        GameObject SpawnRoom;


        // Start is called before the first frame update
        void Start()
        {
            GenerateSpawnRoom();
            GenerateMainPath();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void GenerateSpawnRoom() 
        {
            GameObject roomToSpawn = Instantiate(GetRandomRoom
                (StarterRooms.RoomsToUse));
            roomToSpawn.transform.position = Vector3.zero;
            SpawnRoom = roomToSpawn;
            GeneratedRooms.Add(roomToSpawn);
        }
        public void GenerateMainPath() 
        {
            Doorway currentDoor = GetRandomDoor(SpawnRoom.GetComponent<Room>());
            for (int i = 0; i < (int)DungeonSize/2; i++)
            {
                GameObject SpawnedRoom = GenerateAndReturnSuitableRoom(currentDoor, 2);
                currentDoor.IsUsed = true;
                currentDoor = GetRandomDoor(SpawnedRoom.GetComponent<Room>());
            }
        }




        #region Door Functions
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

            public Heading InvertDoorDirection(Heading heading) 
            {
                switch (heading)
                {
                    case Heading.North:
                        return Heading.South;
                    case Heading.East:
                        return Heading.West;
                    case Heading.South:
                        return Heading.North;
                    case Heading.West:
                        return Heading.East;
                    default:
                        Debug.LogError("Returned a non-existant door!");
                        return Heading.North;
                }
            }

        #endregion

        #region DeterminationFunctions     
        public GameObject GetRandomRoom(List<GameObject> candidateRooms) 
        {
            int randomSelection = Random.Range(0, candidateRooms.Count);
            return candidateRooms[randomSelection];
        }

        public Doorway GetRandomDoor(Room room) 
        {
            List<Doorway> validDoors = new List<Doorway>();
            foreach (var item in room.doorways)
            {
                if (!item.IsUsed)
                {
                    validDoors.Add(item);
                }
            }
            int randomSelection = Random.Range(0, validDoors.Count);
            return validDoors[randomSelection];
        }

        public Doorway GetRandomUnobstructedDoor(Room room) 
        {
            Doorway doorToReturn;
            List<Doorway> validDoors = new List<Doorway>();
            foreach (var item in room.doorways)
            {
                if (!item.IsUsed)
                {
                    validDoors.Add(item);
                }
            }
            int randomSelection = Random.Range(0, validDoors.Count);
            Collider[] hits = Physics.OverlapBox(validDoors[randomSelection]
                .GetNextRoomPlace().position, Vector3.one);
            if (!hits.Contains(null))
            {
                doorToReturn = GetRandomUnobstructedDoor(room);
            }
            else
            {
                doorToReturn = validDoors[randomSelection];
            }
            return doorToReturn;
        }

        #endregion

        #region GenerativeFunctions
        //0 criteria
        public void GenerateSuitableRoom(Doorway connection) 
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.RoomsToUse)
            {
                var RoomRef = item.GetComponent<Room>();
                if (RoomRef.doorways.Any(D => D.Facing == connectionSide))
                {
                    suitableRooms.Add(item);
                }
            }

            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;         
        }

        //Number of Doors criteria
        public void GenerateSuitableRoom(Doorway connection, int minimumDoorNumber)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.RoomsToUse)
            {
                var RoomRef = item.GetComponent<Room>();
                if ((RoomRef.doorways.Any(D => D.Facing == connectionSide)) 
                    && (RoomRef.doorways.Count >= minimumDoorNumber))
                {
                    suitableRooms.Add(item);
                }
            }

            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
        }

        //Number of Doors criteria, Returns GO
        public GameObject GenerateAndReturnSuitableRoom(Doorway connection, int minimumDoorNumber)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.RoomsToUse)
            {
                var RoomRef = item.GetComponent<Room>();
                if ((RoomRef.doorways.Any(D => D.Facing == connectionSide))
                    && (RoomRef.doorways.Count >= minimumDoorNumber))
                {
                    suitableRooms.Add(item);
                }
            }





            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
            GeneratedRooms.Add(roomToSpawn);
            return roomToSpawn;
        }

        #endregion

    }
}

