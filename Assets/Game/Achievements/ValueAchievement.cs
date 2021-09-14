using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Achievements
{
    public abstract class ValueAchievement : Achievement
    {
        public int TargetValue = 15;
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

