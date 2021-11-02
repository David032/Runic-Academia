using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cardinal;

namespace Runic.SceneManagement
{
    public class Loader : MonoBehaviour
    {
        protected GameObject Player;
        protected GameObject MainCam;
        protected GameObject VirtualCam;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            MainCam = GameObject.FindGameObjectWithTag("MainCamera");
            VirtualCam = GameObject.FindGameObjectWithTag("VirtualCamera");
        }

        protected IEnumerator LoadPlayerIntoLoadingScene()
        {
            if (Player is null)
            {
                Player = GameObject.FindGameObjectWithTag("Player");
            }
            if (MainCam is null)
            {
                MainCam = GameObject.FindGameObjectWithTag("MainCamera");
            }
            if (VirtualCam is null)
            {
                VirtualCam = GameObject.FindGameObjectWithTag("VirtualCamera");
            }

            print("At load  of loadign scene start:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
            AsyncOperation LoadLoadingScene = SceneManager.LoadSceneAsync
                ("LoadingScene", LoadSceneMode.Additive);
            while (!LoadLoadingScene.isDone)
            {
                yield return null;
            }
            SceneManager.MoveGameObjectToScene(Player, SceneManager.GetSceneAt(1));
            SceneManager.MoveGameObjectToScene(MainCam, SceneManager.GetSceneAt(1));
            SceneManager.MoveGameObjectToScene(VirtualCam, SceneManager.GetSceneAt(1));
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));

            GameObject PlayerHoldingLocation = GameObject.Find("PlayerMarker");
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = PlayerHoldingLocation.transform.position;
            yield return new WaitForSeconds(1f);
            Player.GetComponent<CharacterController>().enabled = true;
            print("At end of loading scene start:");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                print("Scene " + i + " is " + SceneManager.GetSceneAt(i).name);
            }
            //[ToFix]StateManager.Instance.ChangeState(GameState.Loading);
        }
    }
}

