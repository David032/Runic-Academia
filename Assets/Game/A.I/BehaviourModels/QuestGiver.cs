using Runic.Dialogue;
using Runic.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Runic.AI
{
    public class QuestGiver : BehaviourBase
    {
        public DialogueObject QuestInitiationDialogue;
        public Quest quest;
        public GameObject icon;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<PlayerControls>().isInteracting)
                {
                    other.GetComponent<PlayerControls>().Interact
                        (InteractionTypes.Person);
                    icon.SetActive(false);
                    Quest questToGive = Instantiate(quest);
                    foreach (Task item in questToGive.TasksToComplete.ToList())
                    {
                        questToGive.TasksToComplete.Remove(item);
                        questToGive.TasksToComplete.Add(Instantiate(item));
                    }
                    TaskManager.Instance.ActiveQuest = questToGive;
                    DialogueManager.Instance.ConfigureDialogue(QuestInitiationDialogue);
                    DialogueManager.Instance.ShowWindow();
                }
            }
        }
    }
}  

