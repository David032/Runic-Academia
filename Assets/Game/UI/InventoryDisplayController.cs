using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Entities;
using TMPro;
using UnityEngine.UI;

namespace Runic.UI
{
    public class InventoryDisplayController : MonoBehaviour
    {
        public GameObject Window;
        public GameObject InventoryItemWidget;
        public TextMeshProUGUI FallBackText;
        public TextMeshProUGUI PlayerCoins;
        public Image SlottedConsumable;

        Entities.Player.Player Owner;
        List<GameObject> displayedItems = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            Owner = GetComponentInParent<Entities.Player.Player>();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerCoins.text = Owner.Coins + "gp";
            SlottedConsumable.sprite = Owner.SlottedConsumable.icon;
        }

        public void ToggleInventoryWindow() 
        {
            Window.SetActive(!Window.activeSelf);
            if (Window.activeSelf)
            {
                foreach (Items.Item item in Owner.Inventory)
                {
                    //FallBackText.text += "\n" + item.Name + " - " + item.Description + " - "
                    //    + item.Type.ToString() + " - " + item.value.ToString() + "gp";
                    GameObject itemDisplay = Instantiate(InventoryItemWidget, Window.transform);
                    ItemWidget widget = itemDisplay.GetComponent<ItemWidget>();
                    widget.SetItemWidget(item.Name, item.Description, item.Type, item.value);


                    displayedItems.Add(itemDisplay);
                }
            }
            else
            {
                FallBackText.text = "";
                foreach (var item in displayedItems)
                {
                    Destroy(item);
                }
            }
        }
    }
}

