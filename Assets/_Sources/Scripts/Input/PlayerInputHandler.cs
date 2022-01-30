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
    //[SerializeField] private float switchButtonPressCd = .2f;

    [Header("Action Button")]
    [SerializeField] private ButtonInputUI actionButton;
    [SerializeField] private Sprite[] actionButtonImages; //0 attack, 1 pick-up
    [SerializeField] private ActionInputMode actionButtonMode; //0 attack, 1 pick-up
    //[SerializeField] private float pickUpButtonPressCd = .2f;
    //[SerializeField] private float attackButtonPressCd = .2f;

    public bool IsSwitchWeaponButtonPressed { get; private set; }
    public bool IsPickUpButtonPressed { get; private set; }
    public bool IsAttackButtonPressedUp { get; private set; }
    public bool IsAttackButtonPressedDown { get; private set; }
    //private float _pressSwitchButtonTimer;
    //private float _pressAttackButtonTimer;
    //private float _pressPickUpButtonTimer;

    //private bool _attackButtonNeedToBeDelayed;

    private void Awake()
    {
      
    }

    private void Start()
    {
        _inputService = Game.InputService;
        ChangeActionModeOnPickUp();
    }

    private void Update()
    {
        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            MovementPosX = _inputService.Axis.x;
            MovementPosY = _inputService.Axis.y;
        }
        

        CheckIfSwitchWeaponButtonPressed();
        CheckIfPickUpButtonPressed();
        CheckIfAttackButtonPressedUp();
        CheckIfAttackButtonPressedDown();
        CkeckIfJoystickPressed();
    }
    public bool CkeckIfJoystickPressed()
    {
        return _inputService.Axis.sqrMagnitude > Constants.Epsilon;
    }
    private void CheckIfPickUpButtonPressed()
    {
        if (actionButtonMode == ActionInputMode.PickUp)
        {
            IsPickUpButtonPressed = _inputService.IsActionButtonUp();
        }

       
        /*IsPickUpButtonPressed = false;
        _pressPickUpButtonTimer += Time.deltaTime;
        if (actionButton.Pressed && _pressPickUpButtonTimer >= pickUpButtonPressCd)
        {
            IsPickUpButtonPressed = true;
            _pressPickUpButtonTimer = 0f;
        }*/
    }

    private void CheckIfAttackButtonPressedDown()
    {
        if (actionButtonMode == ActionInputMode.Attack)
        {
           IsAttackButtonPressedDown =  _inputService.IsActionButtonDown();
            /* if (actionButton.Pressed && !_attackButtonNeedToBeDelayed)
             {
                 IsAttackButtonPressed = true;
             }
             else
             {
                 IsAttackButtonPressed = false;
             }*/
        }
    }
    private void CheckIfAttackButtonPressedUp()
    {
        if (actionButtonMode == ActionInputMode.Attack)
        {
           IsAttackButtonPressedUp =  _inputService.IsActionButtonUp();
        }
    }


    private void CheckIfSwitchWeaponButtonPressed()
    {
        /*IsSwitchWeaponButtonPressed = false;
        _pressSwitchButtonTimer += Time.deltaTime;
        if (switchWeaponButton.Pressed && _pressSwitchButtonTimer >= switchButtonPressCd)
        {
            IsSwitchWeaponButtonPressed = true;
            _pressSwitchButtonTimer = 0f;
        }*/
        IsSwitchWeaponButtonPressed = _inputService.IsSwitchWeaponButtonPressed();
    }

    
    public void ChangeActionModeOnAttack()
    {
        // it fires instantly 
        // hmm

        StartCoroutine(WaitAndChangeActionModeOnAttack());
        
    }

 
    private IEnumerator WaitAndChangeActionModeOnAttack()
    {
        actionButton.GetComponent<Image>().sprite = actionButtonImages[0];
        yield return new WaitForSeconds(.5f);
        actionButtonMode = ActionInputMode.Attack;
       
    }


    public void ChangeActionModeOnPickUp()
    {
        actionButtonMode = ActionInputMode.PickUp;
        actionButton.GetComponent<Image>().sprite = actionButtonImages[1];
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
