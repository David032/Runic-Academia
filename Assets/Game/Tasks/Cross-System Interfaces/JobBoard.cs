using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Dialogue;
using Cardinal.Appraiser;

namespace Runic.Tasks.Interfaces
{
    public class JobBoard : MonoBehaviour
    {
        public List<Task> PotentialTasks;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<PlayerControls>().isInteracting)
                {
                    other.GetComponent<PlayerControls>().Interact
                        (InteractionTypes.Chest); //Need a noticeboard animation
                    int RandomSelection = Random.Range(0, PotentialTasks.Count);
                    Task taskToGrant = PotentialTasks[RandomSelection];
                    if (taskToGrant is Endeavour)
                    {
                        TaskManager.Instance.ActiveEndeavours
                            .Add(Instantiate((Endeavour)taskToGrant));
                        DialogueObject TaskMessage = new DialogueObject();
                        TaskMessage.Contents = "You took the Endeavour " + taskToGrant.name;
                        DialogueObject TaskMessageDescription = new DialogueObject();
                        TaskMessageDescription.Contents = taskToGrant.Description;
                        TaskMessage.NextMessage = TaskMessageDescription;
                        DialogueManager.Instance.ConfigureDialogue(TaskMessage);
                        DialogueManager.Instance.ShowWindow();
                        PotentialTasks.Remove(taskToGrant);
                    }
                    if (taskToGrant is Job)
                    {
                        TaskManager.Instance.ActiveJobs
                            .Add(Instantiate((Job)taskToGrant));
                        DialogueObject TaskMessage = new DialogueObject();
                        TaskMessage.Contents = "You took the job " + taskToGrant.name;
                        DialogueObject TaskMessageDescription = new DialogueObject();
                        TaskMessageDescription.Contents = taskToGrant.Description;
                        TaskMessage.NextMessage = TaskMessageDescription;
                        DialogueManager.Instance.ConfigureDialogue(TaskMessage);
                        DialogueManager.Instance.ShowWindow();
                        PotentialTasks.Remove(taskToGrant);
                    }
                    if (taskToGrant is Quest)
                    {
                        if (TaskManager.Instance.ActiveQuest is null)
                        {
                            TaskManager.Instance.ActiveQuest
                                = Instantiate((Quest)taskToGrant);
                            DialogueObject TaskMessage = new DialogueObject();
                            TaskMessage.Contents = "You took the quest " +taskToGrant.name;
                            DialogueObject TaskMessageDescription = new DialogueObject();
                            TaskMessageDescription.Contents = taskToGrant.Description;
                            TaskMessage.NextMessage = TaskMessageDescription;
                            DialogueManager.Instance.ConfigureDialogue(TaskMessage);
                            DialogueManager.Instance.ShowWindow();
                            PotentialTasks.Remove(taskToGrant);
                        }
                        else
                        {
                            DialogueObject ErrorMessage = new DialogueObject();
                            ErrorMessage.Contents = "You already have a quest!";
                            DialogueManager.Instance.ConfigureDialogue(ErrorMessage);
                            DialogueManager.Instance.ShowWindow();
                        }
                    }

                    TaskTakenEvent @event = ScriptableObject.CreateInstance<TaskTakenEvent>();
                    @event.Name = "Player took task " + taskToGrant.Name;
                    @event.Time = Time.realtimeSinceStartup.ToString();
                    @event.EventPriority = Cardinal.Priority.Medium;
                    @event.Task = taskToGrant;
                    @event.Correleation = new HexadCorrelation(Cardinal.HexadTypes.Players, 300);
                    Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);
                }
            }
        }
    }
}

