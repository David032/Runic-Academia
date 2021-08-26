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
        //Do we want a boss room?
        public bool RequiresBoss = true;
        [Header("Data Objects")]
        public RoomList RoomList;
        public RoomList StarterRooms;
        public RoomList BossRooms;
        [Header("Generated Data")]
        public List<GameObject> GeneratedRooms;
        int RoomsGenerated = 0;

        [Header("Internals")]
        GameObject SpawnRoom;
        GameObject BossRoom;
        GameObject priorRoom;

        // Start is called before the first frame update
        void Start()
        {
            if (DungeonType == TypeOfDungeon.Branching)
            {
                GenerateSpawnRoom();
                GenerateMainPath();
            }

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
            priorRoom = SpawnRoom;
        }
        public void GenerateMainPath() 
        {
            Doorway currentDoor = GetRandomDoor(SpawnRoom.GetComponent<Room>());
            currentDoor.ActivateDoorway();
            currentDoor.IsUsed = true;
            for (int i = 0; i < ((int)DungeonSize/2)-1; i++)
            {
                GameObject SpawnedRoom = 
                    GenerateAndReturnSuitableRoom(currentDoor, 2);
                priorRoom = SpawnedRoom;
                currentDoor = GetRandomDoor(SpawnedRoom.GetComponent<Room>());
            }

            if (RequiresBoss)
            {
                BossRoom = GenerateSpecialRoom(currentDoor, BossRooms);
                //Don't update prior room as it's end of line
            }

            #region Error Checking
            GeneratedRooms.Reverse();
            List<GameObject> DeactivatedRooms = new List<GameObject>();

            //Identify Overlapping Rooms
            foreach (GameObject item in GeneratedRooms)
            {
                List<GameObject> roomsTocheck = GeneratedRooms;
                foreach (GameObject room in roomsTocheck)
                {
                    Room thisRoom = item.GetComponent<Room>();
                    bool isStartOrEnd = thisRoom.RoomFlags.Contains
                        (RoomFlags.StartingRoom) || 
                        thisRoom.RoomFlags.Contains(RoomFlags.BossRoom);

                    if (Vector3.Distance(item.transform.position,
                        room.transform.position) < 5 
                        && !(item == room) && !(isStartOrEnd))
                    {
                        print(item + " and " + room + " appear to be overlapping at " 
                            + room.transform.position);
                        print("With a spacing of " + 
                            Vector3.Distance(item.transform.position,
                            room.transform.position));
                        item.SetActive(false);
                        DeactivatedRooms.Add(item);
                    }
                }
            }

            bool toggle = false;
            foreach (GameObject item in DeactivatedRooms)
            {
                if (!toggle)
                {
                    item.SetActive(true);
                    toggle = true;
                }
                else
                {
                    toggle = false;
                }
            }
            #endregion
        }
        public void GenerateSecondaryPaths() 
        {
            List<Doorway> AvailableDoors;
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
                room.doorways[randomDoorSelection].DisableDoor();
                room.doorways[randomDoorSelection].IsUsed = true;
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

        public Doorway GetExclusionaryRandomDoor(Room room, Doorway doorToIgnore)
        {
            List<Doorway> validDoors = new List<Doorway>();
            foreach (var item in room.doorways)
            {
                if (!item.IsUsed && !doorToIgnore)
                {
                    validDoors.Add(item);
                }
            }
            int randomSelection = Random.Range(0, validDoors.Count);
            return validDoors[randomSelection];
        }

        public Doorway GetRandomClearDoor(Room room)
        {
            List<Doorway> validDoors = new List<Doorway>();
            foreach (var item in room.doorways)
            {
                bool isClear = false;

                Collider[] colliders = Physics.OverlapBox
                    (item.GetNextRoomPlace().position, Vector3.one);
                if (colliders != null)
                {
                    isClear = true;
                }
                else
                {
                    print(room + " is not clear, on doorway " + item);
                }
                if (!item.IsUsed && isClear)
                {
                    validDoors.Add(item);
                }
            }
            int randomSelection = Random.Range(0, validDoors.Count);
            return validDoors[randomSelection];
        }

        public bool TestForOverlap(Vector3 loc) 
        {
            Collider[] colliders = Physics.OverlapBox
                (loc, Vector3.one);
            if (colliders != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public GameObject GenerateAndReturnSuitableRoom
            (Doorway connection, int minimumDoorNumber)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.RoomsToUse)
            {
                var RoomRef = item.GetComponent<Room>();
                if ((RoomRef.doorways.Any
                    (D => D.Facing == InvertDoorDirection(connectionSide))
                    && (RoomRef.doorways.Count >= minimumDoorNumber)))
                {
                    suitableRooms.Add(item);
                }
            }
            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
            roomToSpawn.transform.rotation.eulerAngles.Set(0, 0, 0);
            roomToSpawn.transform.rotation = Quaternion.EulerAngles(0, 0, 0);

            Room roomToSpawnData = roomToSpawn.GetComponent<Room>();
            foreach (Doorway item in roomToSpawnData.doorways)
            {
                if (item.Facing == InvertDoorDirection(connectionSide))
                {
                    item.IsUsed = true;
                    item.DisableDoor();
                    connection.IsUsed = true;
                    if (!priorRoom.GetComponent<Room>().RoomFlags
                        .Contains(RoomFlags.StartingRoom))
                    {
                        connection.DisableDoor();
                    }
                }
            }
            GeneratedRooms.Add(roomToSpawn);
            return roomToSpawn;
        }
        public GameObject TestGenerateAndReturnSuitableRoom
            (Doorway connection, int minimumDoorNumber)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.RoomsToUse)
            {
                var RoomRef = item.GetComponent<Room>();
                if ((RoomRef.doorways.Any
                    (D => D.Facing == InvertDoorDirection(connectionSide))
                    && (RoomRef.doorways.Count >= minimumDoorNumber)))
                {
                    suitableRooms.Add(item);
                }
            }

            //Check if connection okay
            Collider[] colliders = Physics.OverlapBox
                (connection.GetNextRoomPlace().position, Vector3.one);
            if (colliders != null)
            {
                //GameObject roomToSpawn = Instantiate
                //    (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
                //roomToSpawn.transform.parent = null;
                //print(colliders);

                //foreach (Doorway item in roomToSpawn.GetComponent<Room>().doorways)
                //{
                //    if (item.Facing == InvertDoorDirection(connectionSide))
                //    {
                //        item.IsUsed = true;
                //    }
                //}
                //GeneratedRooms.Add(roomToSpawn);
                //roomToSpawn.GetComponent<Room>().isBroken = true;
                return null;
            }
            else
            {
                GameObject roomToSpawn = Instantiate
                    (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
                roomToSpawn.transform.parent = null;

                foreach (Doorway item in roomToSpawn.GetComponent<Room>().doorways)
                {
                    if (item.Facing == InvertDoorDirection(connectionSide))
                    {
                        item.IsUsed = true;
                    }
                }
                GeneratedRooms.Add(roomToSpawn);
                return roomToSpawn;
            }
        }

        //Special Room Generation
        public GameObject GenerateSpecialRoom
            (Doorway connection, RoomList specialRoomList)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in specialRoomList.RoomsToUse)
            {
                var RoomRef = item.GetComponent<Room>();
                if (RoomRef.doorways.Any
                    (D => D.Facing == InvertDoorDirection(connectionSide)))
                {
                    suitableRooms.Add(item);
                }
            }

            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
            roomToSpawn.transform.rotation.eulerAngles.Set(0, 0, 0);
            roomToSpawn.transform.rotation = Quaternion.EulerAngles(0, 0, 0);

            Room roomToSpawnData = roomToSpawn.GetComponent<Room>();
            foreach (Doorway item in roomToSpawnData.doorways)
            {
                if (item.Facing == InvertDoorDirection(connectionSide))
                {
                    item.IsUsed = true;
                    item.ActivateDoorway();
                    connection.IsUsed = true;
                    connection.ActivateDoorway();
                }
            }
            GeneratedRooms.Add(roomToSpawn);
            return roomToSpawn;
        }
        #endregion

    }
}

