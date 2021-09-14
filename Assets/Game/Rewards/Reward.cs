using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Rewards
{
    public abstract class Reward : ScriptableObject
    {
        public string RewardName = "";
        public string RewardDescription = "";
        public int RewardValue = -1;
        public abstract void OnRecieve();
    }
}

