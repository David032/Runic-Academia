using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic.Entities;

namespace Runic.UI
{
    public class InventoryDisplayController : MonoBehaviour
    {
        public GameObject Window;
        public GameObject InventoryItemWidget;

        Entity Owner;
        List<GameObject> displayedItems = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            Owner = GetComponentInParent<Entity>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Window.activeSelf)
            {
                foreach (Items.Item item in Owner.Inventory)
                {
                    GameObject itemDisplay = 
                        Instantiate(InventoryItemWidget, Window.transform);
                    try
                    {
                        itemDisplay.GetComponent<ItemWidget>().SetItemWidget
                            (item.name, item.Description, item.Type, item.value);
                    }
                    catch (System.Exception)
                    {
                        print("?!?!?!");
                    }

                    displayedItems.Add(itemDisplay);
                }
            }
            else
            {
                if (displayedItems.Count != 0)
                {
                    foreach (var item in displayedItems)
                    {
                        Destroy(item.gameObject);
                    }
                }

            }
        }

        public void ToggleInventoryWindow() 
        {
            Window.SetActive(!Window.activeSelf);
        }
    }
}

