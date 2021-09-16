using Runic.Rewards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Tasks 
{
    [CreateAssetMenu(fileName = "Endeavour", menuName = "Runic/Tasks/Endeavour")]
    public class Endeavour : Task
    {
        public Reward Reward;

        public void GiveReward() 
        {
            Reward.OnRecieve();
        }

        public override void OnCompletion()
        {
            if (TaskManager.Instance.ActiveEndeavours.Contains(this))
            {
                TaskManager.Instance.ActiveEndeavours.Remove(this);
                CompletionMessage();
            }
            else
            {
                if (TaskManager.Instance.ActiveQuest.TasksToComplete.Contains(this))
                {
                    TaskManager.Instance.ActiveQuest.CompleteStage(this);
                    CompletionMessage();
                }
            }
            TaskManager.Instance.CompletedTasks.Add(this);
        }
    }
}

