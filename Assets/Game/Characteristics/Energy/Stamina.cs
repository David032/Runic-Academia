using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Characteristics
{
    public interface IEnduranceBonus
    {
        int GetEnduranceBonus(int baseEndurance);
    }

    public class Stamina : Energy
    {
        public int baseEndurance = 10;
        public int bonusEndurance = 0;

        IEnduranceBonus[] _bonusComponents;
        IEnduranceBonus[] bonusComponents
        {
            get 
            { 
                return _bonusComponents ??= GetComponents<IEnduranceBonus>(); 
            }
        }

        public override int max
        {
            get
            {
                int baseThisLevel = baseEndurance;
                int bonus = bonusEndurance;
                return baseThisLevel + bonus;
            }
        }
    }
}

