using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private FixedTouchField touchField;
    [SerializeField] private FixedTouchField attackButton;
    [SerializeField] private FixedButton switchWeaponButton;

    private Vector2 _movementPos; 

    public float MovementPosX { get; private set; }
    public float MovementPosY { get; private set; }

    //public bool _isJoystickPressed { get; private set; }
    public bool IsAttackButtonPressed { get; private set; }
    public bool IsSwitchWeaponButtonPressed { get; private set; }

    private float _pressButtonTimer;
    [SerializeField] private float _switchWeaponButtonCd = .3f;

    private void Update()
    {
        MovementPosX = joystick.Horizontal;
        MovementPosY = joystick.Vertical;

        //_isPlayerMoving = _movementPosX != 0.0f && _movementPosY != 0.0f;
        //_isJoystickPressed = touchField.Pressed;
        IsAttackButtonPressed = attackButton.Pressed;

        IsSwitchWeaponButtonPressed = false;
        _pressButtonTimer += Time.deltaTime;
        if (switchWeaponButton.Pressed && _pressButtonTimer >= _switchWeaponButtonCd)
        {
            IsSwitchWeaponButtonPressed = true;
            _pressButtonTimer = 0f;
        }
      
        
       
    }

}
