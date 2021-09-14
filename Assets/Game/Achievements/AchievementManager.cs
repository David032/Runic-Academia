using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Achievements
{
    public class AchievementManager : MonoSingleton<AchievementManager>
    {
        public Dictionary<Achievement, bool> ActiveAchievements = new Dictionary<Achievement, bool>();
        public List<Achievement> CompletedAchievements = new List<Achievement>();

        void Start()
        {
            Object[] Achievements;
            Achievements = Resources.LoadAll("", typeof(Achievement));
            foreach (var item in Achievements)
            {
                //Should this be a copy, or should this actually be the live one?
                ActiveAchievements.Add(Instantiate((Achievement)item), false); 
                print("Added " + (Achievement)item + " to the manager!");
            }
        }
    }
}

