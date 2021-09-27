using Cardinal.Analyser;
using Cardinal.Appraiser;
using Cardinal.Generative.Dungeon;
using Runic.Entities.Player;
using Runic.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runic.Interactions
{
    [RequireComponent(typeof(BoxCollider))]
    public class ResourceController : MonoBehaviour
    {
        public List<Item> itemsInNode = new List<Item>();
        private void Awake()
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<BoxCollider>().size = Vector3.one * 2;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag != "Player")
            {
                return;
            }

            if (other.GetComponent<PlayerControls>().isInteracting)
            {
                other.GetComponent<PlayerControls>().Interact(InteractionTypes.Chest);
                NodeInteractedWith @event = ScriptableObject.CreateInstance<NodeInteractedWith>();
                @event.Name = "Player harvested item(s)";
                @event.Time = Time.realtimeSinceStartup.ToString();
                @event.EventPriority = Cardinal.Priority.Low;
                @event.NodeType = Cardinal.NodeType.Chest;
                @event.items = itemsInNode.ToList();
                @event.RoomType = GetComponentInParent<Room>().Type;
                if (Tasks.TaskManager.Instance.HasItemTasks())
                {
                    @event.Correleation = new HexadCorrelation(Cardinal.HexadTypes.Achievers, 100);
                }
                else
                {
                    @event.Correleation = new HexadCorrelation(Cardinal.HexadTypes.FreeSpirits, 100);
                }
                Analyser.Instance.RegisterEvent(@event);

                foreach (var item in itemsInNode)
                {
                    other.GetComponent<Player>().inventory.AddItem(item);
                    Tasks.TaskManager.Instance.CheckForUpdates();
                }
                itemsInNode.Clear();
            }
        }
    }
}

