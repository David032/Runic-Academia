using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Rewards
{
    [CreateAssetMenu(fileName = "CurrencyReward", menuName = "Runic/Rewards/CurrencyReward")]
    public class CurrencyReward : Reward
    {
        public int AmountToGain = 0;
        public override void OnRecieve()
        {
            //Add Amount to currency pouch            
        }
    }
}

