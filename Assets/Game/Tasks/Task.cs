using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Tasks
{
    public abstract class Task : ScriptableObject
    {
        public string Name = "";
        public string Description = "";
        public bool isQuestElement = false;

        public abstract void OnCompletion();
    }
}

