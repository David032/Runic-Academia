using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Rewards
{
    [CreateAssetMenu(fileName = "ItemReward", menuName = "Runic/Rewards/ItemReward")]
    public class ItemReward : Reward
    {
        public Items.Item ItemToReward;

        public override void OnRecieve()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Entities.Entity PlayerEntity = player.GetComponent<Entities.Entity>();
            PlayerEntity.inventory.AddItem(Instantiate(ItemToReward));
            DisplayRewardMessage();
        }
    }
}

