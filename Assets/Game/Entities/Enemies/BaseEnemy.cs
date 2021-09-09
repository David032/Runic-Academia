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

        public Vector3 GenerateRandomPosition(Vector3 origin, float range)
        {
            Vector3 destination = new Vector3();
            destination.x = Random.Range(origin.x + (range * -1),
                origin.x + range);
            destination.y = origin.y;
            destination.z = Random.Range(origin.z + (range * -1),
                origin.z + range);
            return destination;
        }

        public GameObject GetPlayerRef() { return playerRef; }
    }

}
