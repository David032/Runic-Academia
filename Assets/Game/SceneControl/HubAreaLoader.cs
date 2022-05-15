using Cardinal.Adjustor;
using Cardinal.Analyser;
using Cardinal.Appraiser;
using Cardinal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace Runic.SceneManagement
{
    public class HubAreaLoader : Loader
    {
        public GameObject Door;
        public List<GameObject> Torches;
        public bool IsBossRoomExit = false;

        private void Start()
        {
            GetComponent<BoxCollider>().enabled = false;
            if (!IsBossRoomExit)
            {
                OpenExit();
            }
        }
        public void OpenExit() 
        {
            Door.transform.eulerAngles.Set(0, 90, 0);
            foreach (GameObject item in Torches)
            {
                item.SetActive(true);
            }
            GetComponent<BoxCollider>().enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            SafetyCheck();
            MainCam.GetComponent<Camera>().cullingMask = LayersToRender;
            if (other.CompareTag("Player"))
            {
                Analyser.Instance.ReflectiveAnalysis();
                if (IsBossRoomExit)
                {
                    Adjustor.Instance.Message(ResponseSubject.Player, 
                        ResponseAction.Completed, 
                        ResponseLocation.CurrentDungeon);
                }
                VirtualCam.GetComponent<CinemachineVirtualCamera>()
                    .GetCinemachineComponent<CinemachineFramingTransposer>()
                    .m_CameraDistance = 10;
                StartCoroutine(LoadHubArea());
            }
        }

        IEnumerator LoadHubArea() 
        {
            if (Tasks.TaskManager.Instance.HasCompleteDungeonTasks())
            {
                CompletedDungeonEvent @event = ScriptableObject
                    .CreateInstance<CompletedDungeonEvent>();
                @event.Name = "Player completed a dungeon";
                @event.Time = Time.realtimeSinceStartup.ToString();
                @event.EventPriority = Cardinal.Priority.High;
                @event.Correleation = new HexadCorrelation
                    (HexadTypes.Players, 300);
                Tasks.TaskManager.Instance.IncrementProgressJobs
                    (ProgressCriteria.DungeonCompletion);
            }
            else
            {
                CompletedDungeonEvent @event = ScriptableObject
                    .CreateInstance<CompletedDungeonEvent>();
                @event.Name = "Player completed a dungeon";
                @event.Time = Time.realtimeSinceStartup.ToString();
                @event.EventPriority = Cardinal.Priority.High;
                @event.Correleation = new HexadCorrelation
                    (HexadTypes.FreeSpirits, 300);
            }
            StartCoroutine(LoadPlayerIntoLoadingScene());
            yield return new WaitForSeconds(5f);
            StartCoroutine(LoadHub()); //LoadHubAreas
            yield return new WaitForSeconds(10f);
            StartCoroutine(UnloadAreas());
        }

        IEnumerator LoadHub() 
        {
            print("At start of loading hub:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
            AsyncOperation LoadHubScene = SceneManager.LoadSceneAsync
                (1, LoadSceneMode.Additive);
            while (!LoadHubScene.isDone)
            {
                yield return null;
            }
            SceneManager.MoveGameObjectToScene(Player, SceneManager.GetSceneAt(2));
            SceneManager.MoveGameObjectToScene(MainCam, SceneManager.GetSceneAt(2));
            SceneManager.MoveGameObjectToScene(VirtualCam, SceneManager.GetSceneAt(2));
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(2));

            GameObject PlayerHoldingLocation = GameObject.Find("HubReturnPosition");
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerHoldingLocation.transform.position;
            yield return new WaitForSeconds(1f);
            Player.GetComponent<CharacterController>().enabled = true;
            print("At end of loading hub:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
            StateManager.Instance.ChangeState(GameState.Hub);
        }

        IEnumerator UnloadAreas()
        {
            print("At end of unload of dungeon & load:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print(SceneManager.GetSceneAt(i).name);
            }
            AsyncOperation UnloadLoading = SceneManager.UnloadSceneAsync
                (SceneManager.GetSceneByName("LoadingScene"));
            while (!UnloadLoading.isDone)
            {
                yield return null;
            }
            print("Unloaded Loading Area!");

            AsyncOperation UnloadHub = SceneManager.UnloadSceneAsync
                (SceneManager.GetSceneByName("GeneratedDungeon"));
            while (!UnloadHub.isDone)
            {
                print(UnloadHub.progress);
                yield return null;
            }
            print("Unloaded Dungeon!");
            print("At end of unload of dungeon & load:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}

