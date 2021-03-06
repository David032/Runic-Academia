using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Entities;
using Runic.Entities.Player;
using UnityEngine.UI;
using Cardinal.Appraiser;

namespace Runic.UI
{
    public class TradeDisplayController : MonoBehaviour
    {
        public GameObject TradeWindow;
        public GameObject PlayerSide;
        public GameObject TraderSide;

        public GameObject InventoryItemWidget;

        List<GameObject> PlayerItems = new List<GameObject>();
        List<GameObject> TraderItems = new List<GameObject>();
        public void ShowTradeDisplay(Entity Trader, Player Player) 
        {
            TradeWindow.SetActive(true);
            foreach (Items.Item item in Player.inventory.Inventory)
            {
                GameObject itemDisplay = 
                    Instantiate(InventoryItemWidget, PlayerSide.transform);
                ItemWidget widget = itemDisplay.GetComponent<ItemWidget>();
                widget.SetItemWidget
                    (item.Name, item.Description, item.Type, item.value);
                widget.GetComponent<Button>().onClick.AddListener
                    (delegate 
                    {
                        OnTrade(Trader, Player, 
                            (item.value * GetTraderBuyingModifier(Trader)), item); 
                    });
                PlayerItems.Add(itemDisplay);
            }
            foreach (Items.Item item in Trader.inventory.Inventory)
            {
                GameObject itemDisplay = 
                    Instantiate(InventoryItemWidget, TraderSide.transform);
                ItemWidget widget = itemDisplay.GetComponent<ItemWidget>();
                widget.SetItemWidget
                    (item.Name, item.Description, item.Type, item.value);
                widget.GetComponent<Button>().onClick.AddListener
                    (delegate 
                    {
                        OnTrade(Player, Trader,
                            item.value * GetTraderSellingModifier(Trader), item); 
                    });
                TraderItems.Add(itemDisplay);
            }
        }

        void OnTrade(Entity DestinationEntity, Entity Seller, float cost, Items.Item itemToTrade) 
        {
            int ActualCost = Mathf.RoundToInt(cost);
            if (DestinationEntity.inventory.Coins - ActualCost >= 0)
            {
                Seller.inventory.RemoveItem(itemToTrade);
                DestinationEntity.inventory.AddItem(itemToTrade);
                DestinationEntity.inventory.RemoveCurrency(ActualCost);
                Seller.inventory.AddCurrency(ActualCost);
            }
            else
            {
                print("Don't think " + DestinationEntity + " has enough coins");
            }
            NPCTradeEvent @event = ScriptableObject.CreateInstance<NPCTradeEvent>();
            @event.Name = "Player made a trade";
            @event.Time = Time.realtimeSinceStartup.ToString();
            @event.EventPriority = Cardinal.Priority.Medium;
            @event.Correleation = new HexadCorrelation(Cardinal.HexadTypes.Philanthropists, 200);
            @event.secondaryCorrelation = new HexadCorrelation(Cardinal.HexadTypes.Players, 200);
            if (Seller.gameObject.CompareTag("Player"))
            {
                @event.ChangeData = new InventoryChangeData(Cardinal.InventoryChange.Loss, itemToTrade, ActualCost);
                @event.NPC = DestinationEntity;
            }
            else
            {
                @event.ChangeData = new InventoryChangeData(Cardinal.InventoryChange.Gain, itemToTrade, ActualCost);
                @event.NPC = Seller;
            }
            Cardinal.Analyser.Analyser.Instance.RegisterEvent(@event);

            ClearDisplay();
            TradeWindow.SetActive(false);
        }

        float GetTraderSellingModifier(Entity Trader) 
        {
            float OpinionOfPlayer = Trader.GetComponent<Cardinal.AI.NPC.NPCMentalModel>().opinion;
            if (OpinionOfPlayer == 0)
            {
                return 1;
            }
            else if (OpinionOfPlayer < 0.5f)
            {
                return 1.5f;
            }
            else if (OpinionOfPlayer >= 0.5f)
            {
                return 0.5f;
            }
            else
            {
                return 1f;
            }
        }

        float GetTraderBuyingModifier(Entity Trader)
        {
            float OpinionOfPlayer = Trader.GetComponent<Cardinal.AI.NPC.NPCMentalModel>().opinion;
            if (OpinionOfPlayer == 0)
            {
                return 1;
            }
            else if (OpinionOfPlayer < 0.5f)
            {
                return 0.5f;
            }
            else if (OpinionOfPlayer >= 0.5f)
            {
                return 1.5f;
            }
            else
            {
                return 1f;
            }
        }

        void ClearDisplay() 
        {
            foreach (GameObject item in PlayerItems)
            {
                Destroy(item);
            }
            foreach (GameObject item in TraderItems)
            {
                Destroy(item);
            }
        }
        public void CloseWindow() 
        {
            ClearDisplay();
            TradeWindow.SetActive(false);
        }
    }
}

