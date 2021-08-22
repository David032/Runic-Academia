using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Dungeon
{
    public class Doorway : MonoBehaviour
    {
        public GameObject Door;
        public GameObject Wall;
        public GameObject roomPoint;

        Vector3 nextLocation;

        // Start is called before the first frame update
        void Start()
        {
            nextLocation = transform.forward * -10;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public Vector3 GetNextRoomSpawnLocation() 
            { return transform.forward * -25; }


    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 midpoint = new Vector3(transform.right.x, transform.position.y,
                transform.right.z);
            Debug.DrawRay(transform.position, transform.forward * -10, Color.red);
        }
    #endif

    }

}
