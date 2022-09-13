using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementZ : MonoBehaviour
{
    public AnimatorManager animatorManager;
    InputManager inputManager;
    private PlayerInput PlayerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dodgeAction;
    private InputAction shootAction;
    private InputAction aimAction;
    Vector3 move2Direction;
    Transform cameraObject;
    public bool groundedPlayer;
    private Vector3 playerVelocity;
    public Vector2 Input;
       public bool isJumping;
    public float jumpingVelocity;
    public float movementSpeed = 7f;
    private float gravityValue = -9.81f;
    private float jumpHeight = 1.0f;
    public float walkingspeed = 1.5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;
    private InputActionReference movementContol;
    CharacterController characterController;
    
    private void Awake()
   {
       inputManager = GetComponent<InputManager>();
        animatorManager = GetComponent<AnimatorManager>();
        characterController = GetComponent<CharacterController>();
        PlayerInput = GetComponent<PlayerInput>();
        cameraObject = Camera.main.transform;
        jumpAction = PlayerInput.actions["Jump"];
        moveAction = PlayerInput.actions["Move"];
        dodgeAction = PlayerInput.actions["Dodge"];
        shootAction = PlayerInput.actions["Shoot"];
        aimAction = PlayerInput.actions["Aim"];


       

    }
    private void OnEnable()
    {
        aimAction.performed += _ => Aim();
        aimAction.canceled += _ => StopAim();
        shootAction.performed += _ => ShootBow();
        shootAction.canceled += _ => animatorManager.animator.SetBool("isShooting", false);





    }

    private void OnDisable()
    {
        aimAction.performed -= _ => Aim();
        aimAction.canceled -= _ => StopAim();
        shootAction.canceled -= _ => ShootBow();
        shootAction.canceled -= _ => animatorManager.animator.SetBool("isShooting", false);



    }
    private void Aim()
    {

       
        
            animatorManager.isAiming = true;
      
        animatorManager.animator.SetBool("isAiming", animatorManager.isAiming);

        if (shootAction.triggered)
        {
            animatorManager.animator.SetBool("isShooting",true ); ;
        }
        


    }

    private void StopAim()
    {





        animatorManager.isAiming = false;
        animatorManager.isShooting = false;
        animatorManager.animator.SetBool("isAiming", false);
        animatorManager.animator.SetBool("isShooting", false);
        }

    private void ShootBow()
    {
        if (animatorManager.isAiming)
        {
            animatorManager.isShooting = true;
            if (animatorManager.isShooting)
            {
                animatorManager.animator.SetBool("isShooting", true);
                
                
            


            }

        }
      


    }
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
        HandleDodge();
        //HandleAimShoot();
        //HandleShoot();


    }

    private void HandleMovement()
    {

        if (isJumping)
        {
            return;
        }
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer)
        {
            print("CharacterController is grounded");
        }
        else
        {
            print("CharacterController is Not grounded");
        }
        
        //if grounded no downard force/gravity appiled
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Input = moveAction.ReadValue<Vector2>();
        inputManager.verticalInput = Input.x;
        inputManager.horizontalInput = Input.y;
        Vector3 move2Direction = new Vector3(Input.x, 0, Input.y);
        move2Direction = move2Direction.x * cameraObject.right.normalized + move2Direction.z * cameraObject.forward.normalized;
    

        move2Direction.y = 0f;

        if (inputManager.moveAmount >= 0.5f)
        {
            move2Direction = move2Direction * movementSpeed;

        }
        else
        {
            move2Direction = move2Direction * walkingspeed;
        }
        Vector3 movementvelocity = move2Direction;
        characterController.Move(movementvelocity * Time.deltaTime);
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
       



       

    }

    public void HandleAimShoot()
    {

        if ( groundedPlayer)
        {
            animatorManager.isAiming = true;
            animatorManager.animator.SetBool("isAiming", animatorManager.isAiming);          
                //animatorManager.animator.SetBool("isShooting", shootAction.triggered);

            
            print("I am aiming");
        }
        else
        {
            animatorManager.isAiming = false;
            animatorManager.animator.SetBool("isAiming", animatorManager.isAiming);
            animatorManager.animator.SetBool("isShooting", false);

            print("I am not aiming");


        }
        inputManager.Aim_Input = false;
    }

    public void HandleShoot()
    {
        if (shootAction.triggered && groundedPlayer)
        {
            
            print("hi");
        }
    }

    private void HandleRotation()
    {



        if (isJumping)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.Euler(0, cameraObject.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);



    }
    public void HandleJump()
    {

        
        
            if (jumpAction.triggered &&groundedPlayer)
            {
                

                jumpingVelocity = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                playerVelocity = move2Direction;
                playerVelocity.y = jumpingVelocity;
                animatorManager.animator.CrossFade(animatorManager.jumpAnimation, animatorManager.animationPlayTransition);
            if (inputManager.Jump_Input)
            {
                return;
            }
        }
            inputManager.Jump_Input = false;
        


    }

    public void HandleDodge()
    {
        
        
            if (dodgeAction.triggered &&groundedPlayer)
            {
                animatorManager.animator.CrossFade(animatorManager.dodgeAnimation, animatorManager.animationPlayTransition);
            if (inputManager.Dodge_Input)
            {
                return;
            }
                
            }
            inputManager.Dodge_Input = false;
        


    }

}






