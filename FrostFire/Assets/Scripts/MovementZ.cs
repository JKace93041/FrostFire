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
    private InputAction switchAction;
    Vector3 move2Direction;
    Transform cameraObject;
    public bool groundedPlayer;
    private Vector3 playerVelocity;
    public Vector2 Input;
       public bool isJumping;
   public bool controlswitch;
    public float jumpingVelocity;
    public float movementSpeed = 7f;
    private float gravityValue = -9.81f;
    private float jumpHeight = 1.0f;
    public float walkingspeed = 1.5f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 15f;
    private float arrowMissDistance = 25f;
    public GameObject arrowPrefab;
    //public Transform spawnpoint;
    public Transform bowTransform;
    public Transform arrowParent;
    
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
        switchAction = PlayerInput.actions["SwitchMap"];


       

    }
    private void OnEnable()
    {
        switchAction.performed += SwitchMap;
        aimAction.performed += _ => Aim();
        aimAction.canceled += _ => StopAim();
        shootAction.performed += _ => ShootBow();
        shootAction.canceled += _ => animatorManager.animator.SetBool("isShooting", false);





    }

    private void SwitchMap(InputAction.CallbackContext context)
    {
        
        if (controlswitch)

        {

            PlayerInput.SwitchCurrentActionMap("Player");
            controlswitch = false;

        }
        else 
        {
            PlayerInput.SwitchCurrentActionMap("PlayerMageMode");

            controlswitch = true;

          

        }
       
    }

    private void OnDisable()
    {
        switchAction.performed -= SwitchMap;
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
    private void Update()
    {
        print(PlayerInput.currentActionMap.ToString());
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
    public void ThrowArrow()
    {
        RaycastHit hit;
       
        //spawnpoint.position = bowTransform.position;
        GameObject arrow = GameObject.Instantiate(arrowPrefab, bowTransform.transform.position, arrowPrefab.transform.rotation, arrowParent);
        ProjectileController projectileController = arrow.GetComponent<ProjectileController>();
        Debug.DrawRay(cameraObject.position.normalized, cameraObject.forward.normalized, Color.green,15f);
        if (Physics.Raycast(bowTransform.position.normalized, cameraObject.forward.normalized, out hit, Mathf.Infinity))
        {
            //GameObject arrow = GameObject.Instantiate(arrowPrefab, bowTransform.transform.position, arrowPrefab.transform.rotation , arrowParent);
            //ProjectileController projectileController = arrow.GetComponent<ProjectileController>();
            //arrow.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);
            
            projectileController.target = hit.point;
            projectileController.hit = true;
        }
        else
        {
           

            projectileController.target = cameraObject.position.normalized + cameraObject.forward.normalized * arrowMissDistance;
            projectileController.hit = true;
        }
        //GameObject arrow = Instantiate(arrowPrefab, bowTransform.transform.position, arrowPrefab.transform.rotation);
        //arrow.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);
        //ProjectileController projectile = arrow.GetComponent<ProjectileController>();
        //if (Physics.Raycast(cameraObject.position, cameraObject.forward, out hit, Mathf.Infinity))
        //{


        //    projectile.target = hit.point;
        //    projectile.hit = true;
        //}
        //else
        //{
        //    projectile.target = cameraObject.position + cameraObject.forward * arrowMissDistance;
        //    projectile.hit = false;
        //}
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
     
        if (!animatorManager.isAiming)
        {
            move2Direction = move2Direction * movementSpeed;
            //print(movementSpeed);


        }
        if (animatorManager.isAiming)
        {
            move2Direction = move2Direction * walkingspeed;
            //print(walkingspeed);
        }
        //{
        //    move2Direction = move2Direction * walkingspeed;
        //}
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



        //if (isJumping)
        //{
        //    return;
        //}
        if (Input != Vector2.zero || animatorManager.isAiming)
        {
           
            Quaternion targetRotation = Quaternion.Euler(0, cameraObject.eulerAngles.y, 0);
          

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
           

      
        }
       



    }
    public void HandleJump()
    {

        isJumping = inputManager.Jump_Input;
        
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






