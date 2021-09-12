using UnityEngine;
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
        int _current = 0;
        public int current
        {
            get { return Mathf.Min(_current, max); }
            set
            {
                bool emptyBefore = _current == 0;
                _current = Mathf.Clamp(value, 0, max);
                if (_current == 0 && !emptyBefore)
                {
                    onEmpty.Invoke();
                }
            }
        }
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
