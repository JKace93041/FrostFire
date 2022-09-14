using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{

    public GameObject followTransform;
    public Vector2 _look;
    public float rotationPower = 3f;
    public Vector2 _move;
    public Vector3 nextPosition;
    public Quaternion nextRotation;
    MovementZ movementZ;
    // Start is called before the first frame update
    void Start()
    {
        movementZ = GetComponent<MovementZ>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move the player based on the X input on the controller
        transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        if (_move.x == 0 && _move.y == 0)
        {
            nextPosition = transform.position;

            //if (movementZ.a)
            //{
            //    //Set the player rotation based on the look transform
            //    transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
            //    //reset the y rotation of the look transform
            //    f/*ollowTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);*/
            //}

            return;
        }
    }
}
