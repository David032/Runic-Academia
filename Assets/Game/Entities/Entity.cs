using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    [Header("Base")]
    public string Name = "";
    public EntityFlag Flag = EntityFlag.Friendly;
    [Header("Attributes")]
    public Health Health;

    protected NavMeshAgent entityAgent;
    protected Animator entityAnimator;

    Vector3 lastPosition;
    float speed;

    void Awake()
    {
        entityAgent = GetComponent<NavMeshAgent>();
        entityAnimator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public float GetSpeed()
    {
        speed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
        return speed;
    }
}
