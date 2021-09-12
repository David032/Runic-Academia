using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Runic.UI
{
    public class ItemWidget : MonoBehaviour
    {
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Type;
        public TextMeshProUGUI Value;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
        public void SetItemWidget
            (string name,string description, ItemType type, int value) 
        {
            Name.text = name;
            Description.text = description;
            Type.text = type.ToString();
            Value.text = value.ToString();
        }

        public void ConfigureUsableItem(Items.Consumable usableItem, Entities.Entity User) 
        {
            GetComponent<Button>().onClick.AddListener(delegate { usableItem.Use(User); });

        }
    }
}

