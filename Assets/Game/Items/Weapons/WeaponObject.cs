using Runic.Characteristics;
using Runic.Characteristics.Titles;
using Runic.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Runic.Weapons
{
    public abstract class WeaponObject : MonoBehaviour
    {
        public new string name = "";
        public WeaponType Type = WeaponType.Melee1H;
        public int damage = 1;
        public float cooldown = 1.5f;
        public float attackDuration = 1;
        public float WeaponRange;

        public UnityEvent OnAttack;
        public TwoGoArgsEvent OnAttackCollision;

        protected Animator entityAnimator;
        protected bool canAttack = true;
        protected Transform holder;
        protected GameObject root;
        protected Entity Wielder;
        private void Awake()
        {
            entityAnimator = GetComponentInParent<Animator>();
            holder = entityAnimator.gameObject.transform;
            Wielder = holder.GetComponent<Entity>();
            //root = holder.GetComponentInChildren<EntityOrigin>().gameObject; - Don't think this is used anywhere?
            //OnAttack.AddListener(delegate { Attack(); }); -Not sure if this is right?
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
                Entity target = other.GetComponent<Entity>();
                other.GetComponent<Health>().current -= damage;

                if (Wielder.Title is DamageTitle)
                {
                    DamageTitle damageData = (DamageTitle)Wielder.Title;
                    if (damageData.AffectedTypes.Contains(target.typeOfEntity))
                    {
                        other.GetComponent<Health>().current 
                            -= damageData.ExtraDamage;
                    }
                }
            }
        }
        public Transform FetchWielder() { return holder; }
    }

    [System.Serializable]
    public class TwoGoArgsEvent: UnityEvent<GameObject, GameObject> { }
}
