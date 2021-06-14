using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public new string name = "";
    public WeaponType Type = WeaponType.Melee1H;
    public int damage = 1;
    public float cooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
