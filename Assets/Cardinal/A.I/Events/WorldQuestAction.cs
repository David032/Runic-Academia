using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.Events
{

    public class WorldQuestAction : BaseEvent
    {
        public int requiredItem;
        bool playerHasRequiredItem;
        bool isCompleted = false;

        public string questCompletedMessage;

        // Start is called before the first frame update
        void Start()
        {
            AssignElements();
        }

        // Update is called once per frame
        void Update()
        {
            //if (player.GetComponent<PlayerInventory>().Inventory.Capacity != 0)
            //{
            //    foreach (Item thing in player.GetComponent<PlayerInventory>().Inventory)
            //    {
            //        if (thing.getId() == requiredItem)
            //        {
            //            playerHasRequiredItem = true;
            //        }
            //    }
            //}

        }

        private void OnMouseDown()
        {
            if (playerHasRequiredItem && CalculateDistance())
            {
                if (!isCompleted)
                {
                    spawnDialogue(questCompletedMessage);
                    CreateEvent(ObjectType.Visual);
                }

            }
        }
    }
}
