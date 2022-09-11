using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update


    //[SerializeField] CharacterController controller;
    //[SerializeField] float speed = 11f;
    //[SerializeField] float gravity = -9.81f;
    //[SerializeField] LayerMask groundmask;
    //[SerializeField] float jumpheight = 3.5f;

    //bool jump;
    //bool isgrounded;
    //Vector3 verticalVelocity = Vector3.zero;
    //Vector2 horizontalInput;

    //private void Update()
    //{
    //    isgrounded = controller.isGrounded;
    //    if (isgrounded)
    //    {
    //        verticalVelocity.y = 0;
    //    }
         
    //    Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
    //    controller.Move(horizontalVelocity * Time.deltaTime);


    //    //jump height equation: v = sqrt(-2 * jumpheight * gravity
    //    if (jump)
    //    {
    //        if (isgrounded)
    //        {
    //            verticalVelocity.y = Mathf.Sqrt(-2f * jumpheight * gravity);
    //        }
    //        jump = false;
    //    }



    //    verticalVelocity.y += gravity * Time.deltaTime;
    //    controller.Move(verticalVelocity * Time.deltaTime);
    //    print(isgrounded);
    //}

    //public void ReceieveInput(Vector2 _horizontalInput)
    //{



    //    horizontalInput = _horizontalInput;
    //}

    //public void OnJumpPressed()
    //{
    //    jump = true;
    //}



}
