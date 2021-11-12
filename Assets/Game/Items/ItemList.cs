using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Items
{
    [CreateAssetMenu(fileName = "Item List", menuName = "Runic/Item/Item List")]
    public class ItemList : ScriptableObject
    {
        public List<Item> Entries;
    }

}
