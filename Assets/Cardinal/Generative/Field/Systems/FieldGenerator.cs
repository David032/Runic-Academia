using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Field
{
    public class FieldGenerator : Generator
    {
        [Header("Variables")]
        public FieldLocationMix LocationSplit = FieldLocationMix.EvenMix;

        [Header("Data")]
        public List<GameObject> AvailableFields;

        [Header("Internals")]
        GameObject field;


        // Start is called before the first frame update
        void Start()
        {
            GenerateField();
        }

        void GenerateField() 
        {
            int RandomSelection = Random.Range(0, AvailableFields.Count);
            field = Instantiate(AvailableFields[RandomSelection]);
        }
    }
}

