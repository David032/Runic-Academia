using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//This is ugly, but atomised into individual checks to prevent screw-ups
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
            foreach (var item in 
                gameObject.transform.GetComponentsInChildren<Transform>())
            {
                if (item.gameObject.layer == 8) //Interior Floors
                {
                    item.GetComponent<MeshRenderer>().enabled = false;
                }
                if (item.gameObject.layer == 11)//Walls that aren't on the ground floor
                {
                    item.GetComponent<MeshRenderer>().enabled = false;
                }
                if (item.gameObject.layer == 10)//Roofs
                {
                    item.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var item in 
                gameObject.transform.GetComponentsInChildren<Transform>())
            {
                if (item.gameObject.layer == 8) //Interior Floors
                {
                    item.GetComponent<MeshRenderer>().enabled = true;
                }
                if (item.gameObject.layer == 11)//Walls that aren't on the ground floor
                {
                    item.GetComponent<MeshRenderer>().enabled = true;
                }
                if (item.gameObject.layer == 10)//Roofs
                {
                    item.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}
