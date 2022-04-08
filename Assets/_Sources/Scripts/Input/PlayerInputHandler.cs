using System;
using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts;
using _Sources.Scripts.Infrastructure;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Infrastructure.Services.Input;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    //[SerializeField] private Joystick joystick;
    private IInputService _inputService;
    public Vector2 MovementPos { get; private set; }
   //public float MovementPosY { get; private set; }

    [SerializeField] private ButtonInputUI switchWeaponButton;
    [SerializeField] private ButtonInputUI doubleFireButton;
    [SerializeField] private ButtonInputUI regenerateLevel;
    
    public bool IsSwitchWeaponButtonPressed { get; private set; }
    public bool IsPickUpButtonPressed { get; private set; }
    public bool IsAttackButtonPressedUp { get; private set; }
    public bool IsAttackButtonPressed { get; private set; }
    public bool IsAttackButtonPressedDown { get; private set; }
    
    public GameObject _attackButtonGo;
    public GameObject _pickUpButtonGo;
    
    private void OnEnable()
    {
        
    }

    private void Awake()
    {
    _inputService = AllServices.Container.Single<IInputService>();
    
        //Debug.Log("PLAYER AWAKE");

        if (_attackButtonGo == null || _pickUpButtonGo == null)
        {
            _attackButtonGo = GameObject.FindWithTag("AttackButton");
            _pickUpButtonGo = GameObject.FindWithTag("PickUpButton");
        }
       

        //ChangeActionModeOnPickUp();
   
        MovementPos = new Vector2(0,0);

    }

    private void Start()
    {
       
    }

    private void Update()
    {
        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            MovementPos = new Vector2(_inputService.Axis.x,_inputService.Axis.y);
           
        }
        
        IsPickUpButtonPressed = _inputService.IsPickUpButtonUp();
        IsAttackButtonPressedDown =  _inputService.IsAttackButtonDown();
        IsAttackButtonPressedUp =  _inputService.IsAttackButtonUp();
        IsSwitchWeaponButtonPressed = _inputService.IsSwitchWeaponButtonPressed();
        IsAttackButtonPressed = _inputService.IsAttackButtonPressed();
        CkeckIfJoystickPressed();


        if (_inputService.IsAttackButtonDown())
        {
            
        }
       //Debug.Log("Attck  "+IsAttackButtonPressed);
        //Debug.Log("Attck Up "+IsAttackButtonPressedUp);
    }
    public bool CkeckIfJoystickPressed()
    {
        return _inputService.Axis.sqrMagnitude > Constants.Epsilon;
    }

    public void EnableAttackButton()
    {
       // StartCoroutine(WaitAndChangeActionButtonOnAttack());
       _pickUpButtonGo.SetActive(false);
       _attackButtonGo.SetActive(true);
 
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
        
       _attackButtonGo.SetActive(false);
       _pickUpButtonGo.SetActive(true);
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
