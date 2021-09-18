using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Entities.Enemies
{
    public class Orkoid : BaseEnemy
    {
        public float MeleeRange = 10f;
        public GameObject SplashPotion;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void MakeRangedAttack()
        {
            StartCoroutine(ThrowPotion());
        }

        IEnumerator ThrowPotion()
        {
            entityAnimator.SetInteger("WeaponType_int", 10);
            yield return new WaitForSeconds(2.3f);
            GameObject spawnedAttack = Instantiate
                (SplashPotion,playerRef.transform.position + new Vector3(0, 1, 0),
                new Quaternion(0, 0, 0, 0));
            entityAnimator.SetInteger("WeaponType_int", 0);
            yield return new WaitForSeconds(0.2f);
        }
    }
}

