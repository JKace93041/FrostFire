using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;
    int horizontalParameterID;
    int verticalparameterID;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontalParameterID = Animator.StringToHash("Horizontal");
        verticalparameterID = Animator.StringToHash("Vertical");
    }
    public void ControlAnimatorValues(float horizontalMovement, float verticalMovement)
    {

        //Animation snap will force either the walk or running
        float snappedHorizontal;
        float snappedVertical;

        #region SnappedHorizontal
        if (horizontalMovement > 0 && horizontalMovement < .55f)
        {
            snappedHorizontal = 0.5f;

        }
        else if (horizontalMovement > 0.5f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 & horizontalMovement > -.55f )
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region SnappedVeritcal
        if ( verticalMovement> 0 && verticalMovement < .55f)
        {
           snappedVertical = 0.5f;

        }
        else if (verticalMovement > 0.5f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0 & verticalMovement > -.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical= -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion
        //blends animations
        animator.SetFloat(horizontalParameterID, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(verticalparameterID, snappedVertical, 0.1f, Time.deltaTime);

    }
}
