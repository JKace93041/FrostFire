using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementZ : MonoBehaviour
{
    //    InputManager InputManager;
    //      [SerializeField] 
    //    CharacterController controller;
    //      [SerializeField] 
    //    float speed = 11f;
    //      [SerializeField] 
    //    float gravity = -9.81f;
    //      [SerializeField] 
    //    LayerMask groundmask;
    //       [SerializeField] 
    //    float jumpheight = 3.5f;
    //    [SerializeField]
    //    private float playerSpeed = 2.0f;
    //    [SerializeField]
    //    private float jumpHeight = 1.0f;
    //    [SerializeField]
    //    private float gravityValue = -9.81f;
    //    [SerializeField]
    //    private float rotationspeed = 5f;
    //    [SerializeField]
    //    private GameObject bulletPrefab;
    //    [SerializeField]
    //    private Transform barrelTransform;
    //    [SerializeField]
    //    private Transform bulletParent;
    //    [SerializeField]
    //    private float bulletMissDistance = 25f;
    //    [SerializeField]
    //    private float animationSmoothTime = .1f;
    //    [SerializeField]
    //    private float animationPlayTransition = .15f;
    //    //private CharacterController controller;
    //    //private PlayerInput PlayerInput;
    //    private Vector3 playerVelocity;
    //    private bool groundedPlayer;
    //    private bool isAiming;
    //    private Transform cameraTransform;
    //    private Animator animator;
    //    int moveXAnimatorParameterId;
    //    int moveZAnimatorParameterId;
    //    int jumpAnimation;
    //    Vector2 horizontalInput;
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    private void Awake()
   {
       inputManager = GetComponent<InputManager>();
//        animator = GetComponent<Animator>();
//        jumpAnimation = Animator.StringToHash("Jump");
//        moveXAnimatorParameterId = Animator.StringToHash("MoveX");
//        moveZAnimatorParameterId = Animator.StringToHash("MoveZ");
  }

    public void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        
    }


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




