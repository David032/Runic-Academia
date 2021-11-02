using Cardinal;
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
        public override void Execute()
        {
            //[ToFix]StateManager.Instance.OnStateChanged.AddListener(AdjustDungeon);
        }
        public void AdjustDungeon() 
        {
            //[ToFix]if (StateManager.Instance.GameState != GameState.Hub)
            //[ToFix]{
            //[ToFix]return;
            //[ToFix]}
            DungeonLoader = GameObject.Find(DungeonName).GetComponent<DungeonLoader>();
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
            //[ToFix]StateManager.Instance.OnStateChanged.RemoveListener(AdjustDungeon);
        }
    }
}

