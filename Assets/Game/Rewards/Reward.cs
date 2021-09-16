using Runic.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Rewards
{
    public abstract class Reward : ScriptableObject
    {
        public string RewardName = "";
        public string RewardDescription = "";
        public int RewardValue = -1;
        public abstract void OnRecieve();

        public void DisplayRewardMessage() 
        {
            DialogueObject Message = CreateInstance<DialogueObject>();
            Message.Contents = "You recieved " + RewardDescription;
            DialogueManager.Instance.ConfigureDialogue(Message);
            DialogueManager.Instance.ShowWindow();
        }
    }
}

