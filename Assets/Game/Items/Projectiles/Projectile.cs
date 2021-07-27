using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float startSpeed = 98.1f;
    public GameObject sourceEntity;
    Vector3 direction;
    void Awake() 
    {
        StartCoroutine(DelayedFire());
    }

    IEnumerator DelayedFire() 
    {
        yield return new WaitForSeconds(0.15f);
        GetComponent<Rigidbody>().velocity =
            (sourceEntity.transform.forward.normalized * startSpeed); 
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
