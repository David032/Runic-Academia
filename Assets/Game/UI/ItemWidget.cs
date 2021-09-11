using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Runic.UI
{
    public class ItemWidget : MonoBehaviour
    {
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Type;
        public TextMeshProUGUI Value;

        public void SetItemWidget
            (string name,string description, ItemType type, int value) 
        {
            Name.text = name;
            Description.text = description;
            Type.text = type.ToString();
            Value.text = value.ToString();
        }
    }
}

