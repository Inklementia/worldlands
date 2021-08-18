using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private FixedTouchField touchField;
    [SerializeField] private FixedTouchField attackButton;

    //private Vector2 _movementPos;

    public float _movementPosX { get; private set; }
    public float _movementPosY { get; private set; }

    public bool _isPlayerMoving { get; private set; }
    public bool _isJoystickPressed { get; private set; }
    public bool _isAttackButtonPressed { get; private set; }


    private void Update()
    {


        _movementPosX = joystick.Horizontal;
        _movementPosY = joystick.Vertical;

        _isPlayerMoving = _movementPosX != 0.0f && _movementPosY != 0.0f;
        _isJoystickPressed = touchField.Pressed;
        _isAttackButtonPressed = attackButton.Pressed;

    }

   
}
