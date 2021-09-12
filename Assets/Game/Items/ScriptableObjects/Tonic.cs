using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Items
{
    /// <summary>
    /// Tonics are temporary 
    /// </summary>
    [CreateAssetMenu(fileName = "Tonic", menuName = "Runic/Item/Tonic", order = 12)]
    public class Tonic : Consumable
    {
        public float Duration = 15f;

        public int HealthModifier = 0;
        public int ManaModifier = 0;
        public int StaminaModifier = 0;

        public override void Use(Entities.Entity entity)
        {
            entity.StartCoroutine(entity.TimedTonicUse(this));
            base.Use(entity);
        }


    }
}
