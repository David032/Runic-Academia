using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            VirtualCam = GameObject.Find("PlayerVirtualCamOverhead");
        }

        protected IEnumerator LoadPlayerIntoLoadingScene()
        {
            Scene LoadingScene = SceneManager.GetSceneByBuildIndex(0);

            AsyncOperation LoadLoadingScene = SceneManager.LoadSceneAsync
                (0, LoadSceneMode.Additive);
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
        }
    }
}

