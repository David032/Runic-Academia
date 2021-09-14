using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        }
    }
}

