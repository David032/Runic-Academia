using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    [RequireComponent(typeof(BoxCollider))]
    public class EntryEvent : BaseEvent
    {
        public bool Oneshot = true;
        bool hasRan = false;

        private void Awake()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!hasRan)
            {
                if (other.tag != "Player")
                {
                    return;
                }
                LogEvent();
                if (Oneshot)
                {
                    hasRan = true;
                }
            }

        }
    }
}

