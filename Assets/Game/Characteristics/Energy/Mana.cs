using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Characteristics
{
    public interface IManaBonus
    {
        int GetManaBonus(int baseMana);
        int GetManaRecoveryBonus();
    }

    public class Mana : Energy
    {
        public int baseMana = 100;
        public int bonusMana = 0;

        IManaBonus[] _bonusComponents;
        IManaBonus[] bonusComponents
        {
            get 
            {
                return _bonusComponents ??= GetComponents<IManaBonus>(); 
            }
        }

        // calculate max
        public override int max
        {
            get
            {
                int baseThisLevel = baseMana;
                int bonus = bonusMana;
                return baseThisLevel + bonus;
            }
        }
    }
}

