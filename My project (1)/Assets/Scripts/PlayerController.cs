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
    private float rotationspeed = 1.8f;


    private CharacterController controller;
    private PlayerInput PlayerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private InputAction moveAction;
    private InputAction jumpAction;

    private void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();
        controller = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        moveAction = PlayerInput.actions["Move"];        
       jumpAction = PlayerInput.actions["Jump"];

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 Input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(Input.x, 0, Input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized; // moves camera in relation to player 
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

      

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);




        //Rotate towards camera direction
        
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);
    }
        





}