using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Achievements
{
    [CreateAssetMenu(fileName = "KillAchievement", 
        menuName = "Runic/Achievements/Kills")]
    public class KillAchievement : ValueAchievement
    {
        public TypeOfEntity TypeToTrack = TypeOfEntity.All;
    }
}

