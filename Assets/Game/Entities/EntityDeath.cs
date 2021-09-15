using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Runic.Achievements;
using Runic.Tasks;

namespace Runic.Entities
{
    public class EntityDeath : MonoBehaviour
    {
        public void OnEntityDeath() 
        {
            Entity thisEntity = GetComponent<Entity>();

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

            foreach (Job job in TaskManager.Instance.ActiveJobs)
            {
                //if (job is KillJob)
                //{
                //    var KillJob = (KillJob)job;
                //    if (KillJob.TypeToTrack == thisEntity.typeOfEntity)
                //    {
                //        KillJob.IncrementValue();
                //    }
                //}
            }
        }
    }
}

