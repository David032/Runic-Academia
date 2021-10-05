using Runic.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public class DungeonCompletionResponse : Response
    {
        public DungeonLoader DungeonLoader;
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
        }
    }
}

