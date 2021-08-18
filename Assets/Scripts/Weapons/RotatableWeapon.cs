using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class RotatableWeapon : MonoBehaviour
    {
        [SerializeField] private bool rotateOnAttack;
        public PlayerInputHandler inputHandler { get; private set; }

        public float RotateAngle { get; private set; }

        private float _initialAngle;

        private void Awake()
        {
            inputHandler = GetComponentInParent<PlayerInputHandler>();
            _initialAngle = transform.rotation.z;
        }
        private void FixedUpdate()
        {
            if (rotateOnAttack && inputHandler._isAttackButtonPressed && inputHandler._isJoystickPressed)
            {
                RotateWeapon();
            }
            if (inputHandler._isJoystickPressed && !rotateOnAttack)
            {
                RotateWeapon();
            }
        }

        private void RotateWeapon()
        {
            RotateAngle = Mathf.Atan2(inputHandler._movementPosX, inputHandler._movementPosY) * Mathf.Rad2Deg;
            if (RotateAngle < 0)
            {
                RotateAngle = -RotateAngle;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, -((RotateAngle - _initialAngle) - 90));
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -((RotateAngle - _initialAngle) - 90));
            }
        }
    }
}
