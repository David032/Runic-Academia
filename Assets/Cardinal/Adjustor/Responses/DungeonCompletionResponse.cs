using Runic.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cardinal.Adjustor
{
    public class DungeonCompletionResponse : Response
    {
        public DungeonLoader DungeonLoader;
        public string DungeonName = "DemoDungeon";

        private void Update()
        {
            if (DungeonLoader == null && SceneManager.GetActiveScene().name == "HubArea")
            {
                DungeonLoader = GameObject.Find(DungeonName)
                    .GetComponent<DungeonLoader>();
            }
        }

        public override void Execute()
        {
            switch (DungeonLoader.RequestedDungeonSize)
            {
                case Generative.SizeOfDungeon.Small:
                    DungeonLoader.RequestedDungeonSize = Generative.SizeOfDungeon.Medium;
                    break;
                case Generative.SizeOfDungeon.Medium:
                    DungeonLoader.RequestedDungeonSize = Generative.SizeOfDungeon.Large;
                    break;
                case Generative.SizeOfDungeon.Large:
                    break;
                default:
                    break;
            }
            Debug.LogError("Execute dungeon adjustment");
        }
    }
}

