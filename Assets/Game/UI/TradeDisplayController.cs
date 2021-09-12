using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Entities;
using Runic.Entities.Player;
using UnityEngine.UI;

namespace Runic.UI
{
    public class TradeDisplayController : MonoBehaviour
    {
        public GameObject TradeWindow;
        public GameObject PlayerSide;
        public GameObject TraderSide;

        public GameObject InventoryItemWidget;

        List<GameObject> PlayerItems;
        List<GameObject> TraderItems;
        public void ShowTradeDisplay(Entity Trader, Player Player) 
        {
            foreach (Items.Item item in Player.Inventory)
            {
                GameObject itemDisplay = Instantiate(InventoryItemWidget, PlayerSide.transform);
                ItemWidget widget = itemDisplay.GetComponent<ItemWidget>();
                widget.SetItemWidget(item.Name, item.Description, item.Type, item.value);
                widget.GetComponent<Button>().onClick.AddListener
                    (delegate { OnTrade(Trader, Player, (item.value * GetTraderBuyingModifier(Trader)), item); });
                PlayerItems.Add(itemDisplay);
            }
            foreach (Items.Item item in Trader.Inventory)
            {
                GameObject itemDisplay = Instantiate(InventoryItemWidget, TraderSide.transform);
                ItemWidget widget = itemDisplay.GetComponent<ItemWidget>();
                widget.SetItemWidget(item.Name, item.Description, item.Type, item.value);
                widget.GetComponent<Button>().onClick.AddListener
                    (delegate { OnTrade(Player, Trader,item.value * GetTraderSellingModifier(Trader), item); });
                TraderItems.Add(itemDisplay);
            }
        }

        void OnTrade(Entity DestinationEntity, Entity Seller, float cost, Items.Item itemToTrade) 
        {
            int ActualCost = Mathf.RoundToInt(cost);
            if (DestinationEntity.Coins - ActualCost >= 0)
            {
                Seller.Inventory.Remove(itemToTrade);
                DestinationEntity.Inventory.Add(itemToTrade);
                DestinationEntity.Coins -= ActualCost;
            }
            else
            {
                print("Don't think " + DestinationEntity + " has enough coins");
            }
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

