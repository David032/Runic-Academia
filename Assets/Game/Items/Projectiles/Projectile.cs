using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float startSpeed = 1;

    private void Awake()
    {
        GetComponent<Rigidbody>().AddRelativeForce(startSpeed,0,0);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().current -= damage;
            //Destroy(gameObject);
        }
    }
}
