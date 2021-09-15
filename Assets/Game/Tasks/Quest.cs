using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Tasks
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Runic/Tasks/Quest")]
    public class Quest : Task
    {
        public List<Task> TasksToComplete;
        public List<Rewards.Reward> Rewards;

        //Grants rewards to player
        public void OnComplete() 
        {
            foreach (Rewards.Reward item in Rewards)
            {
                item.OnRecieve();
            }
        }

        public void CompleteStage(Task elementCompleted) 
        {
            TasksToComplete.Remove(elementCompleted);
            TaskManager.Instance.CompletedTasks.Add(elementCompleted);
            if (TasksToComplete.Count == 0)
            {
                OnComplete();
                OnCompletion();
            }
        }

        //Clears it's data up
        public override void OnCompletion()
        {
            TaskManager.Instance.ActiveQuest = null;
            TaskManager.Instance.CompletedTasks.Add(this);
        }
    }
}

