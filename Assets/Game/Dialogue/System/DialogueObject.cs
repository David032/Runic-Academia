using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runic.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", 
        menuName = "Runic/Dialogue/Dialogue Object")]
    public class DialogueObject : ScriptableObject
    {
        public string Contents;
        public Sprite Graphic;
        public DialogueObject NextMessage;
    }
}

