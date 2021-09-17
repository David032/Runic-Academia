using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.SceneManagement
{
    public class PlayerSpawner : MonoSingleton<PlayerSpawner>
    {
        public GameObject Player;
        public GameObject VirtualCam;
        public GameObject MainCam;

        private void Start()
        {
            GameObject player = Instantiate(Player);
            player.transform.position = transform.position;
            Instantiate(MainCam);
            GameObject vCam = Instantiate(VirtualCam);
            vCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = 
                GameObject.Find("FollowTarget").transform;

        }
    }
}

