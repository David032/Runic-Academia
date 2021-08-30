using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cardinal.Generative.Field
{
    public class FieldGenerator : Generator
    {
        [Header("Variables")]
        public FieldLocationMix LocationSplit = FieldLocationMix.EvenMix;

        [Header("Data")]
        public InterestPlaceList POIsource;
        public FieldList PotentialFields;

        [Header("Internals")]
        GameObject field;


        // Start is called before the first frame update
        void Start()
        {
            GenerateField();
            PopulateFieldStructures();
        }

        void GenerateField() 
        {
            var AvailableFields = PotentialFields.PotentialFields;
            int RandomSelection = Random.Range(0, AvailableFields.Count);
            field = Instantiate(AvailableFields[RandomSelection]);
        }

        void PopulateFieldStructures() 
        {
            var PlacesToFill = GameObject.FindGameObjectsWithTag("NodeMarker");
            foreach (GameObject item in PlacesToFill)
            {
                FieldNode nodeData = item.GetComponent<FieldNode>();
                print(nodeData);
                List<GameObject> potentialFillers = new List<GameObject>();
                foreach (GameObject filler in POIsource.PotentialPOIs)
                {
                    print(filler);
                    if (filler.GetComponent<InterestNode>().Size <= nodeData.Size)
                    {
                        potentialFillers.Add(filler);
                    }
                }
                int randomSelection = Random.Range(0, potentialFillers.Count);
                GameObject spawnedPOI = Instantiate(potentialFillers[randomSelection], item.transform);

            }
        }

    }
}

