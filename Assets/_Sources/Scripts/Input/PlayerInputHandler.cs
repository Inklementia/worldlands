using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    //[SerializeField] private FixedTouchField touchField;
    [SerializeField] private FixedTouchField actionButton;
    [SerializeField] private FixedTouchField attackButton;
    //[SerializeField] private FixedTouchField doubleAttackButton;
    [SerializeField] private FixedButton switchWeaponButton;

    private Vector2 _movementPos; 

    public float MovementPosX { get; private set; }
    public float MovementPosY { get; private set; }

    //public bool _isJoystickPressed { get; private set; }
    public bool IsActionButtonPressed { get; private set; }
    public bool IsSwitchWeaponButtonPressed { get; private set; }

    private float _pressButtonTimer;
    [SerializeField] private float _switchWeaponButtonCd = .3f;
    [SerializeField] private float _actionButtonCd = .3f;


    private void Start()
    {
        actionButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        MovementPosX = joystick.Horizontal;
        MovementPosY = joystick.Vertical;

        //_isPlayerMoving = _movementPosX != 0.0f && _movementPosY != 0.0f;
        //_isJoystickPressed = touchField.Pressed;

        //IsActionButtonPressed = actionButton.Pressed;

        IsSwitchWeaponButtonPressed = false;
        _pressButtonTimer += Time.deltaTime;
        if (switchWeaponButton.Pressed && _pressButtonTimer >= _switchWeaponButtonCd)
        {
            IsSwitchWeaponButtonPressed = true;
            _pressButtonTimer = 0f;
        }


        IsActionButtonPressed = false;
        _pressButtonTimer += Time.deltaTime;
        if (actionButton.Pressed && _pressButtonTimer >= _actionButtonCd)
        {
            IsActionButtonPressed = true;
            _pressButtonTimer = 0f;
        }

    }
    public void EnableActionButton()
    {
        attackButton.gameObject.SetActive(false);
        actionButton.gameObject.SetActive(true);
    }
    public void DisableActionButton()
    {
        attackButton.gameObject.SetActive(true);
        actionButton.gameObject.SetActive(false);
    }
    public void EnableWeaponSwitchButton()
    {
        switchWeaponButton.gameObject.SetActive(true);
    }
    public void DisableWeaponSwitchButton()
    {
        switchWeaponButton.gameObject.SetActive(false);
    }
}
