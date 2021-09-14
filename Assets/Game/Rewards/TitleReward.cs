using Runic.Characteristics.Titles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Rewards
{
    [CreateAssetMenu(fileName = "TitleReward", menuName = "Runic/Rewards/TitleReward")]
    public class TitleReward : Reward
    {
        public BaseTitle TitleToRecieve;
        public override void OnRecieve()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Entities.Entity PlayerEntity = player.GetComponent<Entities.Entity>();
            PlayerEntity.Title = Instantiate(TitleToRecieve);
        }
    }
}

