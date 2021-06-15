using UnityEngine;
using UnityEngine.Events;

public abstract class Energy : MonoBehaviour
{
    public int current
    {
        get { return current; }
        set
        {
            bool emptyBefore = current == 0;
            current = Mathf.Clamp(value, 0, max);
            if (current == 0 && !emptyBefore) onEmpty.Invoke();
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
    public float Percent() =>
    (current != 0 && max != 0) ? (float)current / (float)max : 0;

    public void Restore()
    {
        current = max;
    }

    [Header("Events")]
    public UnityEvent onEmpty;
}
