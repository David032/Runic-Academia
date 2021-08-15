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

        public int AttackMod = 0;
        public int RangedMod = 0;
        public int MagicMod = 0;
        public int DefMod = 0;

        //public override void Use(Character player)
        //{
        //    PlayerCharacter playerCharacter = (PlayerCharacter)player;
        //    NarrativeManager.Instance.LogMessage("The player drank " + name.ToLower());
        //    playerCharacter.StartCoroutine(playerCharacter.TimedTonicUse(this));
        //    base.Use(player);
        //}


    }
}
