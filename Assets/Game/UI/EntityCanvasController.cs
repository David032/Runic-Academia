using Runic.Characteristics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Runic.Entities;

namespace Runic.UI
{
    public class EntityCanvasController : MonoBehaviour
    {
        public GameObject NamePlate;
        public TextMeshProUGUI EntityNameText;
        Transform cameraLocation;
        Entity Entity;
        void Start()
        {
            Invoke("SetUp", 1.5f);
        }

        void SetUp() 
        {
            cameraLocation = Camera.main.transform;
            Entity = GetComponentInParent<Entity>();
            if (Entity.Name != null)
            {
                EntityNameText.text = Entity.Name;
            }
            else
            {
                NamePlate.SetActive(false);
            }

            if (Entity.Flag == EntityFlag.Passive ||
                Entity.Flag == EntityFlag.Friendly)
            {
                StatBarController[] statBarControllers
                    = GetComponentsInChildren<StatBarController>();
                foreach (var item in statBarControllers)
                {
                    item.gameObject.SetActive(false);
                }
            }
        }

        void Update()
        {
            if (cameraLocation == null)
            {
                cameraLocation = Camera.main.transform;
            }
            transform.LookAt(cameraLocation);
        }
    }
}

