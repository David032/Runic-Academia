using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string Name = "";
    public string Description = "";
    public ItemType Type = ItemType.Accessory;
    public GameObject model;
    public Sprite icon;
    public int value = 1;

}
