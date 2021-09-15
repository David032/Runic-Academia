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

        public void OnCompletion()
        {
            TaskManager.Instance.ActiveJobs.Remove(this);
            TaskManager.Instance.CompletedTasks.Add(this);
        }
    }
}

