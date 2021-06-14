using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        entityAnimator.SetFloat("Speed_f", GetSpeed());
    }
}
