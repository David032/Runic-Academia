using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeapon : WeaponObject
{
    public GameObject projectile;
    protected override void AttackEnd()
    {
        entityAnimator.SetInteger("WeaponType_int", 0);
        entityAnimator.SetFloat("Head_Horizontal_f", 0);
        entityAnimator.SetBool("Shoot_b", false);
        entityAnimator.SetFloat("Body_Horizontal_f", 0);
        entityAnimator.SetFloat("Body_Vertical_f", 0);
    }

    protected override void AttackStart()
    {
        entityAnimator.SetInteger("WeaponType_int", 11);
        entityAnimator.SetBool("Shoot_b", true);
        entityAnimator.SetFloat("Head_Horizontal_f", -1);
        entityAnimator.SetFloat("Body_Horizontal_f", 0.65f);
        entityAnimator.SetFloat("Body_Vertical_f", 0.25f);
        SpawnArrow();
    }

    void SpawnArrow() 
    {
        GameObject spawnedArrow = Instantiate(projectile);
        spawnedArrow.GetComponent<Projectile>().sourceEntity = root;
        spawnedArrow.GetComponent<Arrow>().bow = this;
        spawnedArrow.transform.position = gameObject.transform.position + holder.forward;
        spawnedArrow.transform.rotation = holder.rotation;
    }
}
