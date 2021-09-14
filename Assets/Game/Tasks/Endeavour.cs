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
    }
}

