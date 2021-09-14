using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Achievements
{
    [CreateAssetMenu(fileName = "ProgressAchievement", 
        menuName = "Runic/Achievements/Progression")]
    public class ProgressAchievement : ValueAchievement
    {
        public ProgressCriteria ProgressionRequirement = ProgressCriteria.DungeonCompletion;
    }
}

