using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class RotatableWeapon : MonoBehaviour
    {
        [SerializeField] private bool rotateOnAttack;
        [SerializeField] private float _initialAngle = 90;
        public PlayerInputHandler inputHandler { get; private set; }

        public float RotateAngle { get; private set; }

        public float InitialRotateAngle { get; private set; }


        private void Awake()
        {
            inputHandler = GetComponentInParent<PlayerInputHandler>();
            

        }
        private void Start()
        {
            InitialRotateAngle = _initialAngle;
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
            InitialRotateAngle = Mathf.Atan2(inputHandler._movementPosX, inputHandler._movementPosY) * Mathf.Rad2Deg; 
            //Debug.Log(RotateAngle);
            if (RotateAngle < 0)
            {
                RotateAngle = -RotateAngle;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, -(RotateAngle - _initialAngle));
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -(RotateAngle - _initialAngle));
            }
        }
    }
}
