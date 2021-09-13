using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic
{
    public enum InteractionTypes
    {
        Chest,
        Person
    }
    public enum EntityFlag
    {
        Friendly,
        Agressive,
        Passive,
        PassiveAggressive
    }

    public enum WeaponType
    {
        Melee1H,
        Melee2H,
        Bow,
        Staff
    }
    public enum ItemType
    {
        Weapon,
        Armour,
        Accessory,
        Pocket,
        Food,
        QuestItem,
        Potion,
        Tonic
    }

    public enum TypeOfEnemy
    {
        Artificial,
        Goblin,
        Undead,
        All
    }
}

