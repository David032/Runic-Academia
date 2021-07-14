using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee1hWeapon : WeaponObject
{
    protected override void AttackStart()
    {
        entityAnimator.SetInteger("WeaponType_int", 12);
        entityAnimator.SetInteger("MeleeType_int", 1);
    }
    protected override void AttackEnd()
    {
        entityAnimator.SetInteger("WeaponType_int", 0);
        entityAnimator.SetInteger("MeleeType_int", 0);
    }
}
