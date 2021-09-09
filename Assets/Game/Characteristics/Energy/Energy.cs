﻿using UnityEngine;
using UnityEngine.Events;

namespace Runic.Characteristics
{
    public enum EnergyType
    {
        Health,
        Mana,
        Stamina
    }
    public abstract class Energy : MonoBehaviour
    {
        public int current = 0;
        public abstract int max { get; }
        public bool spawnFull = true;

        protected virtual void Awake() 
        {
            if (spawnFull)
            {
                current = max;
            }
        }

        private void Update()
        {
            if (current <= 0)
            {
                onEmpty.Invoke();
            }
        }
        public float Percent() =>
        (current != 0 && max != 0) ? (float)current / (float)max : 0;

        public void Restore()
        {
            current = max;
        }

        [Header("Events")]
        public UnityEvent onEmpty;
    }

}
