using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Rewards
{
    [CreateAssetMenu(fileName = "EnergyReward", menuName = "Runic/Rewards/EnergyReward")]
    public class EnergyReward : Reward
    {
        public int AmountToGain = 0;
        public Characteristics.EnergyType EnergyTypeToGain = 
            Characteristics.EnergyType.Health;

        public override void OnRecieve()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Entities.Entity PlayerEntity = player.GetComponent<Entities.Entity>();

            switch (EnergyTypeToGain)
            {
                case Characteristics.EnergyType.Health:
                    PlayerEntity.Health.bonusHealth += AmountToGain;
                    break;
                case Characteristics.EnergyType.Mana:
                    PlayerEntity.Mana.bonusMana += AmountToGain;
                    break;
                case Characteristics.EnergyType.Stamina:
                    PlayerEntity.Stamina.bonusEndurance += AmountToGain;
                    break;
                default:
                    break;
            }
            DisplayRewardMessage();
        }
    }
}

