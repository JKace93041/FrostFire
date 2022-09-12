using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementZ : MonoBehaviour
{
    
    InputManager inputManager;
    private PlayerInput PlayerInput;
    private InputAction moveAction;
    Vector3 moveDirection;
    Transform cameraObject;
    public float movementSpeed = 7f;
    public float walkingspeed = 1.5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;
    private InputActionReference movementContol;
    CharacterController characterController;

    private void Awake()
   {
       inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        cameraObject = Camera.main.transform;
       
       

    }


    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();

    }

    private void HandleMovement()
    {
        
       
        moveDirection = cameraObject.forward.normalized * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right.normalized * inputManager.horizontalInput;
        
        moveDirection.y = 0f;

        if (inputManager.moveAmount >= 0.5f)
        {
            moveDirection = moveDirection * movementSpeed;

        }
        else
        {
            moveDirection = moveDirection * walkingspeed;
        }
        Vector3 movementvelocity = moveDirection;
        
        characterController.Move(movementvelocity * Time.deltaTime );



        //    Vector2 movement = movementControl.action.ReadValue<Vector2>();
        //    //currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, Input, ref animationVelocity, animationSmoothTime);
        //    Vector3 move = new Vector3(movement.x, 0, movement.y);
        //    move = move.x * cameraObject.right.normalized + move.z * cameraObject.forward.normalized; // moves camera in relation to player 
        //    move.y = 0f;
        //  characterController.Move(move * Time.deltaTime * movementSpeed);

        //    //blends strafe animation
        //    animator.SetFloat(moveXAnimatorParameterId, currentAnimationBlendVector.x);
        //    animator.SetFloat(moveZAnimatorParameterId, currentAnimationBlendVector.y);

        //    // Changes the height position of the player..
        //    if (jumpAction.triggered && groundedPlayer)
        //    {
        //        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //        animator.CrossFade(jumpAnimation, animationPlayTransition);
        //    }

        //    playerVelocity.y += gravityValue * Time.deltaTime;
        //    controller.Move(playerVelocity * Time.deltaTime);
        //}


        //    // Start is called before the first frame update



        //    private void handlemovement()
        //    {
        //        groundedPlayer = controller.isGrounded;
        //        //if grounded no downard force/gravity appiled
        //        if (groundedPlayer && playerVelocity.y < 0)
        //        {
        //            playerVelocity.y = 0f;
        //        }
        //        Vector2 Input = moveAction.ReadValue<Vector2>();
        //        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, Input, ref animationVelocity, animationSmoothTime);
        //        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        //        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized; // moves camera in relation to player 
        //        move.y = 0f;
        //        controller.Move(move * Time.deltaTime * playerSpeed);

        //        //blends strafe animation
        //        animator.SetFloat(moveXAnimatorParameterId, currentAnimationBlendVector.x);
        //        animator.SetFloat(moveZAnimatorParameterId, currentAnimationBlendVector.y);
        //    }
        //    public void ReceiveInput(Vector2 _horizontalInput)
        //    {
        //         horizontalInput =_horizontalInput;
        //        Vector2 Input = InputManager.Input.ReadValue<



        //    }


        //    // Update is called once per frame
        //    void Update()
        //    {

        //    }
        ////    {
        ////    // Start is called before the first frame update


        ////    [SerializeField] CharacterController controller;
        ////    [SerializeField] float speed = 11f;
        ////    [SerializeField] float gravity = -9.81f;
        ////    [SerializeField] LayerMask groundmask;
        ////    [SerializeField] float jumpheight = 3.5f;

        ////    bool jump;
        ////    bool isgrounded;
        ////    Vector3 verticalVelocity = Vector3.zero;
        ////    Vector2 horizontalInput;

        ////    private void Update()
        ////    {
        ////        isgrounded = controller.isGrounded;
        ////        if (isgrounded)
        ////        {
        ////            verticalVelocity.y = 0;
        ////        }

        ////        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        ////        controller.Move(horizontalVelocity * Time.deltaTime);


        ////        //jump height equation: v = sqrt(-2 * jumpheight * gravity
        ////        if (jump)
        ////        {
        ////            if (isgrounded)
        ////            {
        ////                verticalVelocity.y = Mathf.Sqrt(-2f * jumpheight * gravity);
        ////            }
        ////            jump = false;
        ////        }



        ////        verticalVelocity.y += gravity * Time.deltaTime;
        ////        controller.Move(verticalVelocity * Time.deltaTime);
        ////        print(isgrounded);
        ////    }

        ////    public void ReceieveInput(Vector2 _horizontalInput)
        ////    {



        ////        horizontalInput = _horizontalInput;
        ////    }

        ////    public void OnJumpPressed()
        ////    {
        ////        jump = true;
        ////    }


    }

    private void HandleRotation()
    {

        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
        if (targetDirection ==Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;

    }


    }






