using Runic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Runic.Entities
{
    [System.Serializable]
    public class OneIntEvent: UnityEvent<int> { }

    public class EntityInventory : MonoBehaviour
    {
        [Header("Resources")]
        public List<Item> Inventory = new List<Item>();
        public int Coins = 50;

        [Header("Events")]
        public UnityEvent OnItemAdded;
        public UnityEvent OnItemRemoved;
        public UnityEvent OnCurrencyAdded;
        public UnityEvent OnCurrencyRemoved;

        #region InventoryMethods
        public void AddItem(Item item) 
        {
            Inventory.Add(item);
            OnItemAdded.Invoke();
        }
        public void RemoveItem(Item item) 
        {
            Inventory.Remove(item);
            OnItemRemoved.Invoke();
        }
        #endregion

        #region CurrencyMethods
        public void AddCurrency(int amount) 
        {
            Coins += amount;
            OnCurrencyAdded.Invoke();
        }
        public void RemoveCurrency(int amount)
        {
            Coins -= amount;
            OnCurrencyRemoved.Invoke();
        }
        #endregion

    }
}

