using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableWeapon : MonoBehaviour, IWeaponFeature
{
    [SerializeField] private bool rotateOnAttack;
    public AdditionalWeaponFeatureDataSO rotatable;

    public float RotateAngle { get; private set; }
   
    private Player _player;
    private float _initialAngle;

    private void Awake()
    {
        _initialAngle = transform.rotation.z;
    }
    private void FixedUpdate()
    {
        if (_player != null)
        {
            if (rotateOnAttack && _player.InputHandler.IsAttackButtonPressed && _player.InputHandler.CkeckIfJoystickPressed())
            {
                RotateWeapon();
            }
            if (_player.InputHandler.CkeckIfJoystickPressed() && !rotateOnAttack)
            {
                RotateWeapon();
            }
        }
    }

    private void RotateWeapon()
    {
        RotateAngle = Mathf.Atan2(_player.InputHandler.MovementPosX, _player.InputHandler.MovementPosY) * Mathf.Rad2Deg;
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

    public void Accept(IVisitor visitor)
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        visitor.Visit(this);
       
        //_player.WeaponHandler.Weaponry.CurrentWeapon.Accept(rotatable)
    }
}
