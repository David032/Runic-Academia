using Runic.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal;
using Cardinal;

namespace Cardinal.Adjustor
{
    public class DungeonProgressionResponse : Response
    {
        public DungeonLoader DungeonLoader;
        public string DungeonName = "DemoDungeon";
        public override void Execute()
        {
            StateManager.Instance.OnStateChanged.AddListener(IncreaseDensityOfDungeon);
        }

        public void IncreaseDensityOfDungeon() 
        {
            if (StateManager.Instance.GameState != GameState.Hub)
            {
            return;
            }
            DungeonLoader = GameObject.Find(DungeonName).GetComponent<DungeonLoader>();
            int randomSelection = Random.Range(0, 2);
            if (randomSelection == 0)
            {
                switch (DungeonLoader.RequestedLootNodeSpread)
                {
                    case Generative.ResourceAvailability.None:
                        DungeonLoader.RequestedLootNodeSpread = Generative.ResourceAvailability.Sparse;
                        break;
                    case Generative.ResourceAvailability.Sparse:
                        DungeonLoader.RequestedLootNodeSpread = Generative.ResourceAvailability.Regular;
                        break;
                    case Generative.ResourceAvailability.Regular:
                        DungeonLoader.RequestedLootNodeSpread = Generative.ResourceAvailability.Abundant;
                        break;
                    case Generative.ResourceAvailability.Abundant:
                        DungeonLoader.RequestedLootNodeSpread = Generative.ResourceAvailability.Overflowing;
                        break;
                    case Generative.ResourceAvailability.Overflowing:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (DungeonLoader.RequestedResourceNodeSpread)
                {
                    case Generative.ResourceAvailability.None:
                        DungeonLoader.RequestedResourceNodeSpread = Generative.ResourceAvailability.Sparse;
                        break;
                    case Generative.ResourceAvailability.Sparse:
                        DungeonLoader.RequestedResourceNodeSpread = Generative.ResourceAvailability.Regular;
                        break;
                    case Generative.ResourceAvailability.Regular:
                        DungeonLoader.RequestedResourceNodeSpread = Generative.ResourceAvailability.Abundant;
                        break;
                    case Generative.ResourceAvailability.Abundant:
                        DungeonLoader.RequestedResourceNodeSpread = Generative.ResourceAvailability.Overflowing;
                        break;
                    case Generative.ResourceAvailability.Overflowing:
                        break;
                    default:
                        break;
                }
            }
            StateManager.Instance.OnStateChanged.RemoveListener(IncreaseDensityOfDungeon);
        }
    }

}
