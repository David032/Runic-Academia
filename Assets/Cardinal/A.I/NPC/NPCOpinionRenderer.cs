using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Cardinal.AI.NPC
{
    public class NPCOpinionRenderer : MonoBehaviour
    {
        TextMeshProUGUI opinionDiskNumber;
        SpriteRenderer opinionDisk;

        Transform playerTransform;
        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            opinionDisk = gameObject.GetComponentInChildren<SpriteRenderer>();
            opinionDiskNumber = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void UpdateDisplay(float opinion)
        {
            Vector3 opinionDiskRotation = opinionDisk.transform.rotation.eulerAngles;
            opinionDiskNumber.text = opinion.ToString();
            opinionDisk.transform.rotation.eulerAngles.Set(opinionDiskRotation.x, playerTransform.rotation.eulerAngles.y, opinionDiskRotation.z);
        }

    }
}
