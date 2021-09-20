using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Dungeon
{
    [RequireComponent(typeof(BoxCollider))]
    public class Room : MonoBehaviour
    {
        public List<Doorway> doorways;
        public List<RoomFlags> RoomFlags;
        public bool isDone = false;
        public bool isBroken = false;
        public bool firstEntry = false;
        public bool isMainRoute = false;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<BoxCollider>().size = new Vector3(20, 10, 20);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (!firstEntry)
            {
                //Fire first entry message
            }
            else
            {
                //Fire retrace entry message
            }
        }
    }

}
