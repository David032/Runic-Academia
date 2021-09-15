using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Rewards;

namespace Runic.Tasks
{
    [CreateAssetMenu(fileName = "Job", menuName = "Runic/Tasks/Job")]
    public class Job : Task
    {
        public Reward Reward;

        public void GiveReward()
        {
            Reward.OnRecieve();
        }
        public override void OnCompletion()
        {
            if (TaskManager.Instance.ActiveJobs.Contains(this))
            {
                TaskManager.Instance.ActiveJobs.Remove(this);
            }
            else
            {
                if (TaskManager.Instance.ActiveQuest.TasksToComplete.Contains(this))
                {
                    TaskManager.Instance.ActiveQuest.CompleteStage(this);
                }
            }
            TaskManager.Instance.CompletedTasks.Add(this);
        }
    }
}

