using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        public DialogueObject Dialogue;
        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            if (other.GetComponent<PlayerControls>().isInteracting)
            {
                other.GetComponent<PlayerControls>().Interact
                    (InteractionTypes.Person);
                transform.LookAt(other.transform);
                //Do dialogue things here?
                DialogueManager.Instance.ConfigureDialogue(Dialogue);
                DialogueManager.Instance.ShowWindow();
            }
        }
    }

}
