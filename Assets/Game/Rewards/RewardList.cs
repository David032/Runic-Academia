using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Rewards
{
    [CreateAssetMenu(fileName = "Reward List", menuName = "Runic/Rewards/Reward List")]
    public class RewardList : ScriptableObject
    {
        public List<Reward> Entries;
    }
}

