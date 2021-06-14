using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IManaBonus
{
    int GetManaBonus(int baseMana);
    int GetManaRecoveryBonus();
}

public class Mana : Energy
{
    public int baseMana = 100;

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
            int bonus = 0;
            for (int i = 0; i < bonusComponents.Length; ++i)
            {
                bonus += bonusComponents[i].GetManaBonus(baseThisLevel);
            }
            return baseThisLevel + bonus;
        }
    }
}
