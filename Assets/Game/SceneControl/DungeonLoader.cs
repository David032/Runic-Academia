using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cardinal.Generative;
using Cardinal.Generative.Dungeon;

namespace Runic.SceneManagement
{
    public class DungeonLoader : Loader
    {
        [Header("Parameters")]
        public SizeOfDungeon RequestedDungeonSize = SizeOfDungeon.Small;
        public TypeOfDungeon RequestedDungeonType = TypeOfDungeon.Branching;
        public bool RequestedRequiresBoss = true;
        [Range(0, 4)]
        public int RequestedNumberOfPuzzleRooms = 0;
        [Range(0, 4)]
        public int RequestedNumberOfSpecialRooms = 0;
        public ResourceAvailability RequestedResourceNodeSpread = ResourceAvailability.Regular;
        public ResourceAvailability RequestedLootNodeSpread = ResourceAvailability.Regular;
        public ResourceAvailability RequestedEnemyAmount = ResourceAvailability.Regular;
        public RoomList RequestedRoomList;
        public RoomList RequestedStarterRooms;
        public RoomList RequestedBossRooms;
        public RoomList RequestedPuzzleRooms;
        public RoomList RequestedSpecialRooms;
        public LootableList RequestedResourceNodes;
        public LootableList RequestedLootNodes;
        public EnemyList RequestedEnemyList;
        public EnemyList RequestedBossList;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
            {
                StartCoroutine(TransitionToDungeon());
            }
        }

        IEnumerator TransitionToDungeon() 
        {
            StartCoroutine(LoadPlayerIntoLoadingScene());
            yield return new WaitForSeconds(5f);
            StartCoroutine(LoadDungeon());
            yield return new WaitForSeconds(10f);
            StartCoroutine(UnloadAreas());        
        }

        IEnumerator LoadDungeon()
        {
            print("At start of loading dungeon:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
            AsyncOperation LoadDungeonScene = SceneManager.LoadSceneAsync
                (3, LoadSceneMode.Additive);
            while (!LoadDungeonScene.isDone)
            {
                yield return null;
            }
            SceneManager.MoveGameObjectToScene(Player, SceneManager.GetSceneAt(2));
            SceneManager.MoveGameObjectToScene(MainCam, SceneManager.GetSceneAt(2));
            SceneManager.MoveGameObjectToScene(VirtualCam, SceneManager.GetSceneAt(2));
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(2));

            StartCoroutine(ConfigureDungeon());

            GameObject PlayerHoldingLocation = GameObject.Find("PlayerStartPosition");
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerHoldingLocation.transform.position;
            yield return new WaitForSeconds(1f);
            Player.GetComponent<CharacterController>().enabled = true;
            print("At end of loading dungeon:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
        }
        IEnumerator ConfigureDungeon()
        {
            DungeonGenerator DungeonManager =
                GameObject.Find("DungeonManager").GetComponent<DungeonGenerator>();
            DungeonManager.DungeonSize = RequestedDungeonSize;
            DungeonManager.DungeonType = RequestedDungeonType;
            DungeonManager.RequiresBoss = RequestedRequiresBoss;
            DungeonManager.NumberOfPuzzleRooms = RequestedNumberOfPuzzleRooms;
            DungeonManager.NumberOfSpecialRooms = RequestedNumberOfSpecialRooms;
            DungeonManager.ResourceNodeSpread = RequestedResourceNodeSpread;
            DungeonManager.LootNodeSpread = RequestedLootNodeSpread;
            DungeonManager.EnemyAmount = RequestedEnemyAmount;

            DungeonManager.RoomList = RequestedRoomList;
            DungeonManager.StarterRooms = RequestedStarterRooms;
            DungeonManager.BossRooms = RequestedBossRooms;
            DungeonManager.PuzzleRooms = RequestedPuzzleRooms;
            DungeonManager.SpecialRooms = RequestedSpecialRooms;
            DungeonManager.ResourceNodes = RequestedResourceNodes;
            DungeonManager.LootNodes = RequestedLootNodes;
            DungeonManager.EnemyList = RequestedEnemyList;
            DungeonManager.BossList = RequestedBossList;
            StartCoroutine(DungeonManager.LoadDungeon());
            yield return new WaitForSeconds(0.5f);
        }

        IEnumerator UnloadAreas()
        {
            print("At start of unload of hub & load:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
            AsyncOperation UnloadLoading = SceneManager.UnloadSceneAsync
                (SceneManager.GetSceneByName("LoadingScene"));
            while (!UnloadLoading.isDone)
            {
                yield return null;
            }
            print("Unloaded Loading Area!");

            AsyncOperation UnloadHub = SceneManager.UnloadSceneAsync
                (SceneManager.GetSceneByName("HubArea"));
            while (!UnloadHub.isDone)
            {
                print(UnloadHub.progress);
                yield return null;
            }
            print("Unloaded Hub Area!");

            print("At end of unload of hub & load:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

