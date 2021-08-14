using Cardinal.AI.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.Events
{
    /// <summary>
    /// Actions where the player having something and interacting with an NPC creates an event
    /// </summary>
    /// 
    [RequireComponent(typeof(NPCDialogue))]
    public class InventoryAction : BaseEvent
    {
        public int requiredItem;
        bool playerHasRequiredItem;

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
                GetComponent<NPCDialogue>().enabled = false;
                spawnDialogue(questCompletedMessage);
                CreateEvent();

                Event eventBeingAdded = GetComponent<EventObject>().LinkedEvent;
                MentalModel.events.Add(eventBeingAdded);

                MentalModel.eventMemories.Add(new NPCEventMemory(eventBeingAdded));
                //Instantiate(spawnables.NPCLearningIcon, this.gameObject.transform);
            }
        }
    }
}
