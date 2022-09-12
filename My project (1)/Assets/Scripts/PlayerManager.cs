using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager InputManager;
    MovementZ playermovement;
    CharacterController characterController;

    private void Awake()
    {
        InputManager = GetComponent<InputManager>();
        playermovement = GetComponent<MovementZ>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }


    private void Update()
    {
        playermovement.groundedPlayer = characterController.isGrounded;
        
        InputManager.HandleAllInputs();
        playermovement.HandleAllMovement();
      
    }

   
}
