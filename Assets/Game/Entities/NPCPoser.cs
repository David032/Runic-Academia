using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Entities
{
    public class NPCPoser : MonoBehaviour
    {
        [Range(-1f,1f)]
        public float HeadVertical = 0;
        [Range(-1f, 1f)]
        public float HeadHorizontal = 0;
        [Range(-1f, 1f)]
        public float BodyVertical = 0;
        [Range(-1f, 1f)]
        public float BodyHorizontal = 0;

        public enum Poses
        {
            Sitting = 9,
            Leaning = 8,
            WipeMouth = 7,
            Salute = 6,
            Smoking = 5,
            Dance = 4,
            CheckWatch = 3,
            HandsOnHips = 2,
            CrossedArms = 1,
            Idle,
        }

        public Poses IdlePose;
        public bool LiveUpdate = false;

        // Start is called before the first frame update
        void Start()
        {
            Animator animator = GetComponent<Animator>();
            animator.SetFloat("Head_Horizontal_f", HeadHorizontal);
            animator.SetFloat("Head_Vertical_f", HeadVertical);
            animator.SetFloat("Body_Horizontal_f", BodyHorizontal);
            animator.SetFloat("Body_Vertical_f", BodyVertical);

            animator.SetInteger("Animation_int", (int)IdlePose);
        }

        private void Update()
        {
            if (LiveUpdate)
            {
                Animator animator = GetComponent<Animator>();
                animator.SetFloat("Head_Horizontal_f", HeadHorizontal);
                animator.SetFloat("Head_Vertical_f", HeadVertical);
                animator.SetFloat("Body_Horizontal_f", BodyHorizontal);
                animator.SetFloat("Body_Vertical_f", BodyVertical);

                animator.SetInteger("Animation_int", (int)IdlePose);
            }

        }
    }

}
