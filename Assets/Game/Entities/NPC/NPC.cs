using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Entities.NPC
{
    public class NPC : Entity
    {
        protected Animator characterAnimator;
        private void Start()
        {
            characterAnimator = GetComponent<Animator>();
        }
        private void Update()
        {
            characterAnimator.SetFloat("Speed_f", GetSpeed());
        }
    }
}

