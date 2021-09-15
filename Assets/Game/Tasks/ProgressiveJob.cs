using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Tasks.Jobs
{
    [CreateAssetMenu(fileName = "ProgressiveJob", menuName = "Runic/Tasks/Job/ProgressiveJob")]
    public class ProgressiveJob : Job
    {
        public ProgressCriteria ProgressCriteria = ProgressCriteria.EnemyKilled;
        public int TargetValue = 5;
        public int CurrentValue = 0;

        public void IncrementValue()
        {
            CurrentValue += 1;
            if (CurrentValue >= TargetValue)
            {
                OnCompletion();
            }
        }
    }
}

