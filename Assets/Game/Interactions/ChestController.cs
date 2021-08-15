using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChestController : MonoBehaviour
{
    public List<Item> itemsInChest = new List<Item>();
    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
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
            foreach (var item in itemsInChest)
            {
                other.GetComponent<Player>().Inventory.Add(item);
            }
            itemsInChest.Clear();
        }
    }
}
