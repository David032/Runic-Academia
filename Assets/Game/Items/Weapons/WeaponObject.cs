using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour
{
    public new string name = "";
    public WeaponType Type = WeaponType.Melee1H;
    public int damage = 1;
    public float cooldown = 1.5f;
    public float attackDuration = 1;

    protected Animator entityAnimator;
    protected bool canAttack = true;
    protected Transform holder;
    private void Awake()
    {
        entityAnimator = GetComponentInParent<Animator>();
        holder = entityAnimator.gameObject.transform;

    }
    protected IEnumerator AttackSequence()
    {
        if (canAttack)
        {
            canAttack = !canAttack;
            GetComponent<Collider>().isTrigger = true;
            AttackStart();
            yield return new WaitForSeconds(attackDuration);
            AttackEnd();
            yield return new WaitForSeconds(cooldown);
            GetComponent<Collider>().isTrigger = false;
            canAttack = true;
        }
    }

    public void Attack() 
    {
        StartCoroutine(AttackSequence());
    }
    protected abstract void AttackStart();
    protected abstract void AttackEnd();

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            other.GetComponent<Health>().current -= damage;
        } 
    }

    public Transform FetchWielder() { return holder; }
}
