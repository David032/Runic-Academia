using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Runic.Interactions
{
    enum DoorState
    {
        Open,
        Closed
    }

    [RequireComponent(typeof(BoxCollider))]
    public class DoorController : MonoBehaviour
    {
        public GameObject Door;
        BoxCollider accessBox;
        DoorState State = DoorState.Closed;
        void Start()
        {
            accessBox = GetComponent<BoxCollider>();
            accessBox.isTrigger = true;
            accessBox.center = new Vector3(-1.25f, 1.25f, 0);
            accessBox.size = new Vector3(2.5f, 2.5f, 1);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                ToggleDoor();
            }
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.GetComponent<PlayerControls>().isInteracting)
                {
                    other.GetComponent<PlayerControls>().Interact
                        (InteractionTypes.Chest); //Need a door animation
                    ToggleDoor();
                }
            }
        }

        void ToggleCam() 
        {

        }

        void ToggleDoor() 
        {
            switch (State)
            {
                case DoorState.Open:
                    Door.transform.Rotate(0, 270, 0);
                    State = DoorState.Closed;
                    break;
                case DoorState.Closed:
                    Door.transform.Rotate(0, 90, 0);
                    State = DoorState.Open;
                    break;
                default:
                    break;
            }
        }
    }
}

