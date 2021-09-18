using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Runic.Entities
{
    public class EntityArmourer : MonoBehaviour
    {
        public GameObject rightHand;
        public GameObject leftHand;
        public AnimatorController controller;

        private void Start()
        {
            if (GetComponent<Animator>().runtimeAnimatorController is null)
            {
                GetComponent<Animator>().runtimeAnimatorController = controller;
            }
        }
    }
}

