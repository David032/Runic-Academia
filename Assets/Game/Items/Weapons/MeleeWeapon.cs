using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponObject
{
    PlayerControls characterControls;
    Entity fighter;
    bool isPlayer;
    bool hasSetup = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSetup)
        {
            fighter = gameObject.GetComponentInParent<Entity>();

            if (gameObject.GetComponentInParent<CharacterController>())
            {
                isPlayer = true;
                characterControls = gameObject.GetComponentInParent<PlayerControls>();
            }
            hasSetup = true;
        }
    }

}
