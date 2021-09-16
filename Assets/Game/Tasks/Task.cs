using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Dialogue;


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
        }
    }
}

