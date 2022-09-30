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
   public bool SpellCast_Input;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animatorManager = GetComponent<AnimatorManager>();
        movementZ = GetComponent<MovementZ>();
        //playerInput.actions.FindActionMap("MageMode").Disable();

    }

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerControls();
       
            controls.Player.Move.performed += ctx => Input = ctx.ReadValue<Vector2>();
            controls.MageMode.Move.performed += ctx => Input = ctx.ReadValue<Vector2>();
            //controls.MageMode.SwitchMap.performed += EnterPlayerMode;
            controls.Player.SpellCast.performed += _ => SpellCast_Input = true;                      
            controls.Player.Jump.performed += ctx => Jump_Input = true;
            controls.Player.Dodge.performed += ctx => Dodge_Input = true;
            controls.Player.Shoot.performed += ctx => Shoot_Input = true;
            controls.Player.Aim.performed += ctx => Aim_Input = true;
    


        }
        controls.Enable();
    }

    private void EnterMageMode(InputAction.CallbackContext context)
    {
        //playerInput.actions.FindActionMap("MageMode").Enable();
        //playerInput.actions.FindActionMap("Player").Disable();
        //playerInput.SwitchCurrentActionMap("MageMode");
        //print(playerInput.currentActionMap.ToString());

        print("hi");

    }

    private void EnterPlayerMode(InputAction.CallbackContext context)
    {
        //playerInput.SwitchCurrentActionMap("Player");
        //print(playerInput.currentActionMap.ToString());

        //    playerInput.actions.FindActionMap("Player").Enable();

        //    playerInput.actions.FindActionMap("MageMode").Disable();

        //    //playerInput.SwitchCurrentActionMap("Player");
        print("bye");

    }

    private void OnDisable()
    {
        
        
        controls.Disable();

       //controls.MageMode.SwitchMap.performed -= EnterPlayerMode;
       // controls.Player.SwitchMap.performed -= EnterMageMode;


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
        HandleSpellCastInput();
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
    private void HandleSpellCastInput()
    {

        if (SpellCast_Input)
        {
            SpellCast_Input = false;
            movementZ.HandleSpellCast();
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

