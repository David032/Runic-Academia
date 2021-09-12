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
        public int ManaModifier = 0;
        public int StaminaModifier = 0;

        public override void Use(Entities.Entity entity)
        {
            if (entity.Health != null)
            {
                entity.Health.current += HealthModifier;
            }
            if (entity.Mana != null)
            {
                entity.Mana.current += ManaModifier;
            }
            if (entity.Stamina != null)
            {
                entity.Stamina.current += StaminaModifier;
            }
            base.Use(entity);
        }
    }
}
