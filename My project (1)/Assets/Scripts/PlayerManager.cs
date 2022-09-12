using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager InputManager;
    MovementZ playermovement;

    private void Awake()
    {
        InputManager = GetComponent<InputManager>();
        playermovement = GetComponent<MovementZ>();

    }


    private void Update()
    {
        InputManager.HandleAllInputs();
        playermovement.HandleAllMovement();
    }

   
}
