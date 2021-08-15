using Runic.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Entities.Enemies
{
    public class TestDummy : BaseEnemy
    {
        WeaponObject equippedWeapon;
        public GameObject equippedWeaponObject;


        // Start is called before the first frame update
        void Start()
        {
            if (GetComponentInChildren<WeaponObject>())
            {
                equippedWeaponObject = GetComponentInChildren<WeaponObject>().gameObject;
                equippedWeapon = equippedWeaponObject.GetComponent<WeaponObject>();
                InvokeRepeating("BasicSwing", 1.5f, equippedWeapon.cooldown);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void BasicSwing() 
        {
            equippedWeapon.Attack();
        }
    }

}
