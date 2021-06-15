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

    private void Awake()
    {
        entityAnimator = GetComponentInParent<Animator>();
    }
    protected IEnumerator AttackSequence()
    {
        AttackStart();
        yield return new WaitForSeconds(attackDuration);
        AttackEnd();
        yield return new WaitForSeconds(cooldown);
    }

    public void Attack() 
    {
        StartCoroutine(AttackSequence());
    }
    protected abstract void AttackStart();
    protected abstract void AttackEnd();

}
