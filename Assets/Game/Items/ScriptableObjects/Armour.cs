using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Items
{
    [CreateAssetMenu(fileName = "Armour", menuName = "Runic/Item/Armour", order = 3)]

    public class Armour : Item
    {
        public int DefenseMod;
        public int HealthMod;

        public int GetDefenseMod() { return DefenseMod; }
    }
}
