using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    enum States
    {
        Casting, Shop, Reeling, Caught
    }


    States playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState = States.Casting;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
