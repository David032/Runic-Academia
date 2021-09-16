using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Runic.Dialogue;

namespace Runic.Achievements
{
    public abstract class Achievement : ScriptableObject
    {
        public string AchievementName = "";
        public string AchievementDescription = "";
        //public System.Guid AchievementId = new System.Guid();

        public void OnCompletion() 
        {
            AchievementManager.Instance.ActiveAchievements[this] = true;
            AchievementManager.Instance.ActiveAchievements.Remove(this);
            AchievementManager.Instance.CompletedAchievements.Add(this);
            DialogueObject Message = CreateInstance<DialogueObject>();
            Message.Contents = "Congratulations! You just unlocked the " 
                + AchievementName + " achievement";
            DialogueManager.Instance.ConfigureDialogue(Message);
            DialogueManager.Instance.ShowWindow();
        }
    }
}

