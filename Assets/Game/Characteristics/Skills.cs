using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Characteristics
{
    public class Skill
    {
        public int currentLevel = 1;
        public int totalLevel = 1;

        public int currentXp = 0;
        public int xpToNextLevel = 83;
        public int totalXp = 0;
        public string name;

        public void GainExperience(int amount) 
        {
            currentXp += amount;
            totalXp += amount;

            if (currentXp >= xpToNextLevel)
            {            
                int excess = currentXp - xpToNextLevel;
                currentLevel += 1;
                totalLevel += 1;
                int increment = Convert.ToInt32((xpToNextLevel / 100) * 10.4);
                xpToNextLevel += increment;
                currentXp = excess;
            }
        }

        public void SetLevel(int level) 
        {
            currentLevel = level;
            totalLevel = level;
            for (int i = 2; i <= level; i++)
            {
                currentXp = xpToNextLevel;
                int increment = Convert.ToInt32((xpToNextLevel / 100) * 10.4);
                xpToNextLevel = xpToNextLevel + increment;
                currentXp = 0;
            }
        }

        public void IncreaseLevel(int increase) 
        {
            currentLevel += increase;
        }
        public void DecreaseLevel(int decrease) 
        {
            currentLevel -= decrease;
        }

        public int[] GetSkillValues() 
        {
            int[] values = new int[] { currentLevel, currentXp, totalLevel, totalXp };
            return values;
        }

        public void SetSkillValues(int[] values) 
        {
            currentLevel = values[0];
            currentXp = values[1];
            totalLevel = values[2];
            totalXp = values[3];
            Debug.Log("Set " + name + " - Current level: " + currentLevel + " Current Xp: " + currentXp
                        + " Total level: " + totalLevel + " Total xp: " + totalXp);
        }

    }

    public class Mind : Skill 
    {
        public Mind() 
        {
            name = "Mind";
        }
    }

    public class Body : Skill 
    {
        public Body() 
        {
            name = "Body";
        }
    }

    public class Soul : Skill 
    {
        public Soul() 
        {
            name = "Soul";
        }
    }
}


