using Cardinal.Appraiser;
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
            if (gameObject.CompareTag("Player"))
            {
                InventoryChangeEvent @event = ScriptableObject.CreateInstance<InventoryChangeEvent>();
                @event.Name = "Player gained " + item.Name;
                @event.Time = Time.realtimeSinceStartup.ToString();
                @event.EventPriority = Cardinal.Priority.Medium;
                @event.Change = Cardinal.InventoryChange.Gain;
                @event.Item = item;
                Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);
            }
            Inventory.Add(item);
            OnItemAdded.Invoke();
        }
        public void RemoveItem(Item item) 
        {
            if (gameObject.CompareTag("Player"))
            {
                InventoryChangeEvent @event = ScriptableObject.CreateInstance<InventoryChangeEvent>();
                @event.Name = "Player lost " + item.Name;
                @event.Time = Time.realtimeSinceStartup.ToString();
                @event.EventPriority = Cardinal.Priority.Medium;
                @event.Change = Cardinal.InventoryChange.Loss;
                @event.Item = item;
                Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);
            }

            Inventory.Remove(item);
            OnItemRemoved.Invoke();
        }
        #endregion

        #region CurrencyMethods
        public void AddCurrency(int amount) 
        {
            if (gameObject.CompareTag("Player"))
            {
                CurrencyChangeEvent @event = ScriptableObject.CreateInstance<CurrencyChangeEvent>();
                @event.Name = "Player gained " + amount + "gp";
                @event.Time = Time.realtimeSinceStartup.ToString();
                @event.EventPriority = Cardinal.Priority.Medium;
                @event.Change = Cardinal.InventoryChange.Gain;
                @event.Amount = amount;
                Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);
            }

            Coins += amount;
            OnCurrencyAdded.Invoke();
        }
        public void RemoveCurrency(int amount)
        {
            if (gameObject.CompareTag("Player"))
            {
                CurrencyChangeEvent @event = ScriptableObject.CreateInstance<CurrencyChangeEvent>();
                @event.Name = "Player lost " + amount + "gp";
                @event.Time = Time.realtimeSinceStartup.ToString();
                @event.EventPriority = Cardinal.Priority.Medium;
                @event.Change = Cardinal.InventoryChange.Loss;
                @event.Amount = amount;
                Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);
            }
            Coins -= amount;
            OnCurrencyRemoved.Invoke();
        }
        #endregion

    }
}

