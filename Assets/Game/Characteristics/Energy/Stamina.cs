using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnduranceBonus
{
    int GetEnduranceBonus(int baseEndurance);
}

public class Stamina : Energy
{
    public int baseEndurance = 10;

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
            int bonus = 0;
            for (int i = 0; i < bonusComponents.Length; ++i)
            {
                bonus += bonusComponents[i].GetEnduranceBonus(baseEndurance);
            }
            return baseEndurance + bonus;
        }
    }
}
