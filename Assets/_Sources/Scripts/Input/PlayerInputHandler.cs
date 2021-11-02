using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    public float MovementPosX { get; private set; }
    public float MovementPosY { get; private set; }

    [SerializeField] private FixedButton switchWeaponButton;
    [SerializeField] private float switchButtonPressCd = .2f;

    [Header("Action Button")]
    [SerializeField] private FixedButton actionButton;
    [SerializeField] private Sprite[] actionButtonImages; //0 attack, 1 pick-up
    [SerializeField] private ActionInputMode actionButtonMode; //0 attack, 1 pick-up
    [SerializeField] private float pickUpButtonPressCd = .2f;

    public bool IsSwitchWeaponButtonPressed { get; private set; }
    public bool IsPickUpButtonPressed { get; private set; }
    public bool IsAttackButtonPressed { get; private set; }

    private float _pressSwitchButtonTimer;
    private float _pressPickUpButtonTimer;
    private void Start()
    {
        ChangeActionModeOnPickUp();
    }

    private void Update()
    {
        MovementPosX = joystick.Horizontal;
        MovementPosY = joystick.Vertical;

        CheckIfSwitchWeaponButtonPressed();
        CheckIfPickUpButtonPressed();
        CheckIfAttckButtonPressed();
        CkeckIfJoystickPressed();
    }
    public bool CkeckIfJoystickPressed()
    {
       return MovementPosX != 0.0f && MovementPosY != 0.0f;
    }
    private void CheckIfPickUpButtonPressed()
    {
        IsPickUpButtonPressed = false;
        _pressPickUpButtonTimer += Time.deltaTime;
        if (actionButton.Pressed && _pressPickUpButtonTimer >= pickUpButtonPressCd)
        {
            IsPickUpButtonPressed = true;
            _pressPickUpButtonTimer = 0f;
        }
    }

    private void CheckIfAttckButtonPressed()
    {
        if (actionButtonMode == ActionInputMode.Attack)
        {
            if (actionButton.Pressed)
            {
                IsAttackButtonPressed = true;
            }
            else
            {
                IsAttackButtonPressed = false;
            }
        }
    }
    private void CheckIfSwitchWeaponButtonPressed()
    {
        IsSwitchWeaponButtonPressed = false;
        _pressSwitchButtonTimer += Time.deltaTime;
        if (switchWeaponButton.Pressed && _pressSwitchButtonTimer >= switchButtonPressCd)
        {
            IsSwitchWeaponButtonPressed = true;
            _pressSwitchButtonTimer = 0f;
        }
    }

    public void ChangeActionModeOnAttack()
    {
        actionButtonMode = ActionInputMode.Attack;
        actionButton.GetComponent<Image>().sprite = actionButtonImages[0];
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
