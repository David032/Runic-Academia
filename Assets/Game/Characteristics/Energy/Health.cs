using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Characteristics
{
    public interface IHealthBonus
    {
        int GetHealthBonus(int baseHealth);
        int GetHealthRecoveryBonus();
    }

    public class Health : Energy
    {
        public int baseHealth = 100;
        public int bonusHealth = 0;

        IHealthBonus[] _bonusComponents;
        IHealthBonus[] bonusComponents
        {
            get 
            { 
                return _bonusComponents ??= GetComponents<IHealthBonus>(); 
            }
        }

        public new void Awake()
        {
            base.Awake();
        }
        public override int max
        {
            get
            {
                int baseThisLevel = baseHealth;
                int bonus = bonusHealth;
                //for (int i = 0; i < bonusComponents.Length; ++i)
                //{
                //    bonus += bonusComponents[i].GetHealthBonus(baseThisLevel);
                //}
                return baseThisLevel + bonus;
            }
        }
    }
}

