using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Entities;

namespace Runic.Tasks.Jobs
{
    [CreateAssetMenu(fileName = "ItemJob", menuName = "Runic/Tasks/Job/ItemJob")]
    public class ItemJob : ProgressiveJob
    {
        public Items.Item TargetItem;
        EntityInventory playerInventory;
        public void CountItems() 
        {
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityInventory>();
            int count = 0;
            foreach (Items.Item thing in playerInventory.Inventory)
            {
                if (thing == TargetItem)
                {
                    count++;
                }
            }
            if (count >= TargetValue)
            {
                OnCompletion();
            }
        }

    }
}

