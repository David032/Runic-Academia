using Runic.Characteristics;
using Runic.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Weapons
{
    public class SplashPotion : MonoBehaviour
    {
        public int Damage = 20;
        public float Duration = 10f;
        private void Start()
        {
            Destroy(gameObject, Duration);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Entity>())
            {
                other.GetComponent<Health>().current -= Damage / 2;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Entity>())
            {
                other.GetComponent<Health>().current -= Damage / 2;
            }
        }
    }

}
