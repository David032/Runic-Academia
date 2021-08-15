using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Runic/Item/Weapon", order = 1)]
    public class Weapon : Item
    {
        public int WeaponMod = 0;
    }
}

