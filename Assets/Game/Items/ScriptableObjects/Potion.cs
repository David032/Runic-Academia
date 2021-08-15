using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Items
{
    /// <summary>
    /// Potions are one-use permenant changes - Health or adreneline
    /// </summary>
    [CreateAssetMenu(fileName = "Potion", menuName = "Runic/Item/Potion", order = 13)]
    public class Potion : Consumable
    {
        public int HealthModifier = 0;
        public int AdrenelineModifier = 0;

        //public override void Use(Character player)
        //{
        //    NarrativeManager.Instance.LogMessage("The player drank " + name.ToLower());
        //    player.ChangeHealth(-1 * HealthModifier);
        //    player.IncreaseAdreneline(AdrenelineModifier);
        //    base.Use(player);
        //}
    }
}
