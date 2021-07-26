using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    public BowWeapon bow;

    new void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().current -= bow.damage;
            base.OnCollisionEnter(collision);
        }
    }
}
