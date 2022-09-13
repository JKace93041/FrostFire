using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    PlayerControls controls;
    AnimatorManager animatorManager;
    //PlayerControls.PlayerActions PlayerActions;
    MovementZ movementZ;
    public Vector2 Input;
    public float moveAmount;
    public float horizontalInput;
    public float verticalInput;
    public bool Jump_Input;
    public bool Dodge_Input;
    public bool Aim_Input;
    public bool Shoot_Input;
    private InputAction aimAction;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animatorManager = GetComponent<AnimatorManager>();
        movementZ = GetComponent<MovementZ>();
        

    }

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerControls();

            controls.Player.Move.performed += ctx => Input = ctx.ReadValue<Vector2>();
            controls.Player.Jump.performed += ctx => Jump_Input = true;
            controls.Player.Dodge.performed += ctx => Dodge_Input = true;
            controls.Player.Shoot.performed += ctx => Shoot_Input = true;
            controls.Player.Aim.performed += ctx => Aim_Input = true;
    


        }
        controls.Enable();
    }

   

    private void OnDisable()
    {
        
        
        controls.Disable();
        



    }
    private void Aim()
    {

        animatorManager.animator.SetBool("isAiming", true);





    }

    private void StopAim()
    {

        animatorManager.animator.SetBool("isAiming", false);




    }
    private void HandleMovement()
    {
        verticalInput = Input.y;
        horizontalInput = Input.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.ControlAnimatorValues(0, moveAmount);



    }
    public void HandleAllInputs()
    {

        HandleMovement();
        HandleJumpingInput();
        HandleDodgeInput();
        //Aim();
        //StopAim();
        
        //HandleAimShoot();
        //HandleShoot();
      
    }

    //private void HandleAimShoot()
    //{
    //    if (Aim_Input)
    //    {
    //        Aim_Input = false;
    //        movementZ.HandleAimShoot();

    //    }
    //}

    ////private void HandleShoot()
    //{
    //    if (Shoot_Input)
    //    {
    //        Shoot_Input = false;
    //        movementZ.
    //    }
    //}

    private void HandleJumpingInput()
    {

        if(Jump_Input)
        {
            Jump_Input = false;
            movementZ.HandleJump();
        }
      

        



    }


    private void HandleDodgeInput()
    {



        if (Dodge_Input)
        {
            Dodge_Input = false;
            movementZ.HandleDodge();

        }


    }


}

