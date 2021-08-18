using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableWeapon : MonoBehaviour
{
    [SerializeField] private float initialAngle = 0;
    public PlayerInputHandler inputHandler { get; private set; }

    public float rotateAngle { get; private set; }

    private void Awake()
    {
        inputHandler = FindObjectOfType<PlayerInputHandler>();
     
    }
    private void FixedUpdate()
    {
        if (inputHandler._isJoystickPressed)
        {
            RotateWeapon();
        }
    }

    private void RotateWeapon()
    {
        rotateAngle = Mathf.Atan2(inputHandler._movementPosX, inputHandler._movementPosY) * Mathf.Rad2Deg;
        if (rotateAngle < 0)
        {
            rotateAngle = -rotateAngle;
            gameObject.transform.rotation = Quaternion.Euler(0, 180, -((rotateAngle - initialAngle) - 90));
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -((rotateAngle - initialAngle) - 90));
        }
    }

}
