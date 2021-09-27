using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cardinal.Appraiser;

//This is ugly, but atomised into individual checks to prevent screw-ups
public class InteriorCameraController : MonoBehaviour
{
    GameObject MainCamObject;
    CinemachineVirtualCamera virtualCamera;

    private void Start()
    {


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

            BuildingEnteredEvent @event = ScriptableObject.CreateInstance<BuildingEnteredEvent>();
            @event.Name = "Player entered " + gameObject;
            @event.Time = Time.realtimeSinceStartup.ToString();
            @event.EventPriority = Cardinal.Priority.Low;
            @event.BuildingName = gameObject.name;
            @event.Correleation = new HexadCorrelation(Cardinal.HexadTypes.FreeSpirits, 100);
            Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);
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
