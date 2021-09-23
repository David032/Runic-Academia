using Cardinal.Appraiser;
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

            TitleEarntEvent @event = ScriptableObject.CreateInstance<TitleEarntEvent>();
            @event.Name = "Player earnt the title " + TitleToRecieve.name;
            @event.Time = Time.realtimeSinceStartup.ToString();
            @event.EventPriority = Cardinal.Priority.Low;
            @event.Title = TitleToRecieve;
            Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);
            DisplayRewardMessage();
        }
    }
}

