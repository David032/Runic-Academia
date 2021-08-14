using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Accessory", menuName = "Runic/Item/Accessory", order = 23)]

public class Accessory : Item
{
    public int AttackMod;
    public int RangedMod;
    public int MagicMod;
    public int DefenseMod;
    public int HealthMod;
}
