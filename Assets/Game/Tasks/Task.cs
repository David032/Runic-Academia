using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Dialogue;
using Cardinal.Appraiser;

namespace Runic.Tasks
{
    public abstract class Task : ScriptableObject
    {
        public string Name = "";
        public string Description = "";
        public bool isQuestElement = false;

        public abstract void OnCompletion();

        public void CompletionMessage() 
        {
            DialogueObject message = new DialogueObject
            {
                Contents = "Congratulations! You completed: " + Name
            };
            DialogueManager.Instance.ConfigureDialogue(message);
            DialogueManager.Instance.ShowWindow();

            TaskCompletedEvent @event = ScriptableObject.CreateInstance<TaskCompletedEvent>();
            @event.Name = "Player completed " + Name;
            @event.Time = Time.realtimeSinceStartup.ToString();
            @event.Task = this;
            @event.EventPriority = Cardinal.Priority.Low;
            @event.Correleation = new HexadCorrelation(Cardinal.HexadTypes.Achievers, 300);
            Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);

        }
    }
}

