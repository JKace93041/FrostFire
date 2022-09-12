using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{


    PlayerControls controls;
    AnimatorManager animatorManager;
    //PlayerControls.PlayerActions PlayerActions;
    MovementZ movementZ;
    public Vector2 Input;
    public float moveAmount;
    public float horizontalInput;
    public float verticalInput;
    public bool Jump_Input;

    private void Awake()
    {
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
           
        }
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
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
      
    }

    private void HandleJumpingInput()
    {

        Jump_Input = true;
        
       
            
    }


    //private void Update()
    //{
    //    movementZ.ReceiveInput(Input);
    //}

    //    [SerializeField]
    //    Movement movement;
    //    [SerializeField]
    //    MouseLook mouseLook;


    //    PlayerControls controls;
    //    PlayerControls.GroundMovementActions groundMovement;

    //    Vector2 horizontalInput;
    //    Vector2 mounseInput;

    //    private void Awake()
    //    {
    //        controls = new PlayerControls();
    //        groundMovement = controls.GroundMovement;


    //        //ctx is the value so if something is a button it doesnt need context since their is no value to read
    //        //these are event lambadas if they cause problems switch later
    //        //groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
    //        //groundMovement.Jump.performed += _ => movement.OnJumpPressed();

    //        groundMovement.MouseX.performed += ctx => mounseInput.x = ctx.ReadValue<float>();
    //        groundMovement.MouseY.performed += ctx => mounseInput.y = ctx.ReadValue<float>();

    //    }


    //    private void OnEnable()
    //    {
    //        controls.Enable();
    //    }


    //    private void OnDisable()
    //    {
    //        controls.Disable();



    //    }

    //    private void Update()
    //    {
    //        movement.ReceieveInput(horizontalInput);
    //        mouseLook.ReceieveInput(mounseInput);
    //    }



}

