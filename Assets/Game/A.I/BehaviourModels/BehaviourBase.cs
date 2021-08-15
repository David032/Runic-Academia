using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Runic.AI
{
    public class BehaviourBase : MonoBehaviour
    {
        NavMeshAgent entityAgent;
        void Start()
        {
            entityAgent = GetComponent<NavMeshAgent>();
        }


    }
}

