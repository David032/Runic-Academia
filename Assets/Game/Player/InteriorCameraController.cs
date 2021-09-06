using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InteriorCameraController : MonoBehaviour
{
    GameObject MainCamObject;
    CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        MainCamObject = GameObject.Find("PlayerVirtualCamOverhead");
        virtualCamera = MainCamObject
            .GetComponent<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainCamObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainCamObject.SetActive(true);
        }
    }
}
