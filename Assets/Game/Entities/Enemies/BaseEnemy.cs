using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Entities.Enemies
{
    public class BaseEnemy : Entity
    {
        public Weapons.WeaponObject weapon;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            entityAnimator.SetFloat("Speed_f", GetSpeed());
        }
    }

}
