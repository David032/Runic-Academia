using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    public string Name = "";
    public EntityFlag Flag = EntityFlag.Friendly;

    NavMeshAgent entityAgent;
    CapsuleCollider entityCollider;
    Animator entityAnimator;

    Vector3 lastPosition;
    float speed;

    void Awake()
    {
        entityCollider = GetComponent<CapsuleCollider>();
        entityAgent = GetComponent<NavMeshAgent>();
        entityAnimator = GetComponent<Animator>();

        entityAnimator.SetInteger("WeaponType_int", 0);
        entityAnimator.SetInteger("MeleeType_int", 0);
    }

    void Update()
    {
        entityAnimator.SetFloat("Speed_f", GetSpeed());
    }

    public float GetSpeed()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
        return speed;
    }
}
