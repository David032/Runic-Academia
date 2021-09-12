using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Items
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "Runic/Item/Consumable", order = 2)]

    public class Consumable : Item
    {
        public virtual void Use(Entities.Entity entity)
        {
            entity.Inventory.Remove(this);
            if (entity.CompareTag("Player"))
            {
                entity.gameObject.GetComponentInChildren<Runic.UI.InventoryDisplayController>().ToggleInventoryWindow();
            }
        }
    }
}
