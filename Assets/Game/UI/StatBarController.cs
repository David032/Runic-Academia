using Runic.Characteristics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runic.UI
{
    public class StatBarController : MonoBehaviour
    {
        public EnergyType EnergyType;
        Energy entityEnergy;
        Slider resourceBar;
        void Start()
        {
            try
            {
                switch (EnergyType)
                {
                    case EnergyType.Health:
                        entityEnergy = GetComponentInParent<Health>();
                        break;
                    case EnergyType.Mana:
                        entityEnergy = GetComponentInParent<Mana>();
                        break;
                    case EnergyType.Stamina:
                        entityEnergy = GetComponentInParent<Stamina>();
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception)
            {
                print("Tried to assign a resource bar to "
                    + gameObject.name + " but failed as there was no"
                    + EnergyType + " !");
            }

            resourceBar = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (entityEnergy)
            {
                resourceBar.value = entityEnergy.Percent();
            }
        }
    }
}

