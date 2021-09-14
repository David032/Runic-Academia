using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Achievements;

namespace Runic.Achievements.Interfaces
{
    public class DungeonCompletion : MonoBehaviour
    {
        public void OnDungeonCompletion()
        {
            foreach (Achievement achievementItem in 
                AchievementManager.Instance.ActiveAchievements.Keys)
            {
                //if (achievementItem is ProgressAchievement progressAchievement)
                //{
                //    if (progressAchievement.ProgressionRequirement ==
                //        ProgressCriteria.DungeonCompletion)
                //    {
                //        progressAchievement.IncrementValue();
                //    }
                //}
            }
        }
    }
}

