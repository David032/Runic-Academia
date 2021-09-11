using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.AI
{
    public class Worker : BehaviourBase
    {
        public List<Transform> WorkLocations;
        public Transform HomeLocation;
        public void GoToWorkLocation() 
        {
            int RandomSelection = Random.Range(0, WorkLocations.Count);
            entityAgent.SetDestination(WorkLocations[RandomSelection].position);
        }
    }
}

