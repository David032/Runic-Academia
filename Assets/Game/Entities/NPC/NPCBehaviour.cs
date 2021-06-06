using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Behaviour", 
    menuName = "Runic Saga/NPC Behaviour")]
public class NPCBehaviour : ScriptableObject
{
    public int WakingHour = 7;
    public int SleepingHour = 23;

}
