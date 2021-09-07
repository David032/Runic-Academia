using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Entities
{
    public class VariantSelector : MonoBehaviour
    {
        public List<GameObject> Variants;

        private void Awake()
        {
            foreach (GameObject item in Variants)
            {
                item.SetActive(false);
            }
            int RandomSelection = Random.Range(0, Variants.Count);
            Variants[RandomSelection].SetActive(true);
        }
    }
}
