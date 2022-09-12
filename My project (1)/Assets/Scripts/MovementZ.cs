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


    }


    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();


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
        //move2Direction = move2Direction + cameraObject.right.normalized * inputManager.horizontalInput; 
        //moveDirection = cameraObject.forward.normalized * inputManager.verticalInput;
        //moveDirection = moveDirection + cameraObject.right.normalized * inputManager.horizontalInput;

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



        if (isJumping)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.Euler(0, cameraObject.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        //Vector3 targetDirection = Vector3.zero;
        //targetDirection = cameraObject.forward * inputManager.verticalInput;
        //targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        //targetDirection.Normalize();
        //targetDirection.y = 0;
        //if (targetDirection ==Vector3.zero)
        //{
        //    targetDirection = transform.forward;
        //}

        //Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        //Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //transform.rotation = playerRotation;

    }
    public void HandleJump()
    {

        if (inputManager.Jump_Input)
        {
            if (jumpAction.triggered &&groundedPlayer)
            {
                

                jumpingVelocity = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                playerVelocity = move2Direction;
                playerVelocity.y = jumpingVelocity;
                animatorManager.animator.CrossFade(animatorManager.jumpAnimation, animatorManager.animationPlayTransition);
            }
            inputManager.Jump_Input = false;
        }

        //if (jumpAction.triggered && groundedPlayer)
        //{
           
            
        //    animatorManager.animator.CrossFade(animatorManager.jumpAnimation , animatorManager.animationPlayTransition);
            
        //    float jumpingVelocity = Mathf.Sqrt(-2 * gravityValue * jumpHeight);
        //    Vector3 playerVelocity = move2Direction;
        //    playerVelocity.y = jumpingVelocity;

            
        //}

    }

}






