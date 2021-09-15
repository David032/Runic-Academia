using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Tasks.Jobs
{
    [CreateAssetMenu(fileName = "KillJob", menuName = "Runic/Tasks/Job/KillJob")]
    public class KillJob : ProgressiveJob
    {
        public TypeOfEntity TypeToTrack = TypeOfEntity.All;
    }
}

