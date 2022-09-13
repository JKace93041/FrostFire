using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVcam : MonoBehaviour
{
   
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int pritorityBoostAmount = 10;
    [SerializeField]
    private Canvas thirdPersonCanvas;
    [SerializeField] 
    private Canvas aimCanvas;
    
    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;

    private void Awake()
    {
        
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];

    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim(); //look up what OnEnable and OnDisable
        
        aimAction.canceled += _ => CancelAim(); //look up what OnEnable and OnDisable

    }
    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim(); 
        aimAction.canceled -= _ => CancelAim(); 
    }


    private void StartAim()
    {
        virtualCamera.Priority += pritorityBoostAmount;
       
        aimCanvas.enabled = true;
        thirdPersonCanvas.enabled = false;
    }

    private void CancelAim()
    {
        virtualCamera.Priority -= pritorityBoostAmount;
       

        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;
    }
}
