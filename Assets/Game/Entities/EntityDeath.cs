using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Runic.Achievements;
using Runic.Tasks;
using Runic.Tasks.Jobs;

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

            foreach (Job jobItem in TaskManager.Instance.ActiveJobs.ToList())
            {
                if (jobItem is KillJob)
                {
                    var KillJob = (KillJob)jobItem;
                    if (KillJob.TypeToTrack == thisEntity.typeOfEntity)
                    {
                        KillJob.IncrementValue();
                    }
                }
            }

            if (TaskManager.Instance.ActiveQuest != null)
            {
                foreach (Task questItem in TaskManager.Instance.ActiveQuest.TasksToComplete)
                {
                    if (questItem is KillJob)
                    {
                        var KillJob = (KillJob)questItem;
                        if (KillJob.TypeToTrack == thisEntity.typeOfEntity)
                        {
                            KillJob.IncrementValue();
                        }
                    }
                }
            }

        }
    }
}

