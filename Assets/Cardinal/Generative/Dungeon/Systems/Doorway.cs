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
        public Heading Facing = Heading.North;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public Transform GetNextRoomPlace() { return roomPoint.transform; }

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
