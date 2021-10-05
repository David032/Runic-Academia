using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public abstract class Response : ScriptableObject
    {
        public abstract void Execute();
    }
}

