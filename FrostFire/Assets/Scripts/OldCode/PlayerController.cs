using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
 
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationspeed = 5f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform barrelTransform;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float bulletMissDistance = 25f;
    [SerializeField]
    private float animationSmoothTime = .1f;
    [SerializeField]
    private float animationPlayTransition = .15f;

    private CharacterController controller;
    private PlayerInput PlayerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isAiming;
    private Transform cameraTransform;
    private PlayerController playerController;

    //Cahced Player input action to avoid using string and making mistakes
    //public InputAction aimAction;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    SwitchVcam Vcam = new SwitchVcam();

    private Animator animator;
    int moveXAnimatorParameterId;
    int moveZAnimatorParameterId;
    int jumpAnimation;

    Vector2 currentAnimationBlendVector;
    Vector2 animationVelocity;

    private void Awake()
    {
        
        PlayerInput = GetComponent<PlayerInput>();
        controller = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
       //cached player inputs
       
        moveAction = PlayerInput.actions["Move"];
        jumpAction = PlayerInput.actions["Jump"];
        shootAction = PlayerInput.actions["Shoot"];
        //Vcam.aimAction = PlayerInput.actions["Aim"];
        
        //locks cursor to middle screen
        Cursor.lockState = CursorLockMode.Locked;
        //animator
        animator = GetComponent<Animator>();
        jumpAnimation = Animator.StringToHash("Jump");
        moveXAnimatorParameterId = Animator.StringToHash("MoveX");
        moveZAnimatorParameterId = Animator.StringToHash("MoveZ");
        

    }
    


    private void OnEnable()
    {
       
        shootAction.performed += _ => ShootGun();

    }
    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();

    }

    private void ShootGun()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {


            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletController.hit = false;
        }
        
        
       
    }
   
    
      
    

    void Update()
    {
        
        groundedPlayer = controller.isGrounded;
        //if grounded no downard force/gravity appiled
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 Input = moveAction.ReadValue<Vector2>();
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, Input, ref animationVelocity, animationSmoothTime);
        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized; // moves camera in relation to player 
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        //blends strafe animation
        animator.SetFloat(moveXAnimatorParameterId, currentAnimationBlendVector.x);
        animator.SetFloat(moveZAnimatorParameterId, currentAnimationBlendVector.y);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.CrossFade(jumpAnimation, animationPlayTransition);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //if (Vcam.aimAction.started && groundedPlayer)
        //{
        //    animator.SetBool("isAiming", true);
        //    print(isAiming);
        //}
        //else
        //{
        //    animator.SetBool("isAiming", false);
        //    print(isAiming);


        //}



        //Rotate towards camera direction

        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);
    }






}