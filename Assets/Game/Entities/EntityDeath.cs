using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Runic.Achievements;
using Runic.Tasks;
using Runic.Tasks.Jobs;
using Cardinal.Appraiser;
using Runic.Entities.Enemies;
using Cardinal.Generative.Dungeon;

namespace Runic.Entities
{
    public class EntityDeath : MonoBehaviour
    {
        public void OnEntityDeath() //We can assume that the player is killing them
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
                foreach (Task questItem in TaskManager.Instance.ActiveQuest.TasksToComplete.ToList())
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

            EnemyKilledEvent @event = ScriptableObject.CreateInstance<EnemyKilledEvent>();
            @event.Name = "Player killed a " + gameObject.name;
            @event.Time = Time.realtimeSinceStartup.ToString();
            @event.EventPriority = Cardinal.Priority.Medium;
            @event.TypeOfEntity = GetComponent<BaseEnemy>().typeOfEntity;
            @event.EnemyCategory = GetComponent<BaseEnemy>().Category;
            @event.RoomType = GetComponentInParent<Room>().Type;
            @event.Correlations.Add(new HexadCorrelation(Cardinal.HexadTypes.Players, 100));
            if (TaskManager.Instance.HasKillTasks(GetComponent<BaseEnemy>().typeOfEntity))
            {
                @event.Correlations.Add(new HexadCorrelation(Cardinal.HexadTypes.Achievers, 100));
            }
            Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);
        }
    }
}

