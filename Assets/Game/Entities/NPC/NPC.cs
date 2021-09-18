using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Entities.NPC
{
    public class NPC : Entity
    {
        private void Start()
        {
        }
        private void Update()
        {
            entityAnimator.SetFloat("Speed_f", GetSpeed());
        }
    }
}

