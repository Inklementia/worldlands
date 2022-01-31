using System;
using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts;
using _Sources.Scripts.Infrastructure;
using _Sources.Scripts.Services.Input;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    //[SerializeField] private Joystick joystick;
    private IInputService _inputService;
    public float MovementPosX { get; private set; }
    public float MovementPosY { get; private set; }

    [SerializeField] private ButtonInputUI switchWeaponButton;

    [Header("Buttons")]
    [SerializeField] private Tag attackButtonGOTag;
    [SerializeField] private Tag pickUpButtonGOTag;
    
    public bool IsSwitchWeaponButtonPressed { get; private set; }
    public bool IsPickUpButtonPressed { get; private set; }
    public bool IsAttackButtonPressedUp { get; private set; }
    public bool IsAttackButtonPressedDown { get; private set; }
    
    private GameObject attackButtonGO;
    private GameObject pickUpButtonGO;
    
    private void Awake()
    {
        _inputService = Game.InputService;
     
        //ChangeActionModeOnPickUp();
        attackButtonGO = gameObject.FindWithTag(attackButtonGOTag);
        pickUpButtonGO = gameObject.FindWithTag(pickUpButtonGOTag);
    }

    private void Update()
    {
        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            MovementPosX = _inputService.Axis.x;
            MovementPosY = _inputService.Axis.y;
        }
        
        IsPickUpButtonPressed = _inputService.IsPickUpButtonUp();
        IsAttackButtonPressedDown =  _inputService.IsAttackButtonDown();
        IsAttackButtonPressedUp =  _inputService.IsAttackButtonUp();
        IsSwitchWeaponButtonPressed = _inputService.IsSwitchWeaponButtonPressed();
 
        CkeckIfJoystickPressed();
    }
    public bool CkeckIfJoystickPressed()
    {
        return _inputService.Axis.sqrMagnitude > Constants.Epsilon;
    }

    public void EnableAttackButton()
    {
       // StartCoroutine(WaitAndChangeActionButtonOnAttack());
       pickUpButtonGO.SetActive(false);
       attackButtonGO.SetActive(true);
 
    }

 
    private IEnumerator WaitAndChangeActionButtonOnAttack()
    {
       // actionButton.GetComponent<Image>().sprite = actionButtonImages[0];
        yield return new WaitForSeconds(.5f);
        //actionButtonMode = ActionInputMode.Attack;
       
    }


    public void EnablePickUpButton()
    {
        //actionButtonMode = ActionInputMode.PickUp;
        //actionButton.GetComponent<Image>().sprite = actionButtonImages[1];
        
       attackButtonGO.SetActive(false);
       pickUpButtonGO.SetActive(true);
        //Debug.Log( actionButton.GetComponent<Image>().sprite.name);
       
    }

    public void EnableWeaponSwitch()
    {
        switchWeaponButton.gameObject.SetActive(true);
    }
    public void DisableWeaponSwitch()
    {
        switchWeaponButton.gameObject.SetActive(true);
    }
}
