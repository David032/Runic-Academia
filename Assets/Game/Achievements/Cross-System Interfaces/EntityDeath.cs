using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Runic.Achievements.Interfaces
{
    public class EntityDeath : MonoBehaviour
    {
        public void OnEntityDeath() 
        {
            Entities.Entity thisEntity = GetComponent<Entities.Entity>();
            foreach (Achievement achievementItem in 
                AchievementManager.Instance.ActiveAchievements.Keys.ToList())
            {
                if (achievementItem is KillAchievement)
                {
                    var killAchievementItem = (KillAchievement)achievementItem;
                    if (killAchievementItem.TypeToTrack == thisEntity.typeOfEntity)
                    {
                        killAchievementItem.IncrementValue();
                    }
                }
            }
        }
    }
}

