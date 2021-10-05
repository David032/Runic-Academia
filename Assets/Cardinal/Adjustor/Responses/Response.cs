using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public abstract class Response : MonoBehaviour
    {
        public ResponseWindow ResponseWindow;
        public abstract void Execute();
    }
}

