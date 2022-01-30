using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts;
using UnityEngine;

public class RotatableWeapon : MonoBehaviour, IWeaponFeature
{
    [SerializeField] private bool rotateOnAttack;
    [SerializeField] private WeaponTypeDataSO rotatableWeaponData;
    [SerializeField] private float initialAngle = 90;

    public float RotateAngle { get; private set; }
    public float InitialRotateAngle { get; private set; }

    private Player _player;
    public WeaponTypeDataSO RotatableWeaponData { get => rotatableWeaponData; private set => rotatableWeaponData = value; }

    private void Awake()
    {
        InitialRotateAngle = initialAngle;
    }
    private void FixedUpdate()
    {
        //Debug.Log(InitialRotateAngle);
        if (_player != null)
        {
            if (rotateOnAttack && _player.InputHandler.IsAttackButtonPressedDown && _player.InputHandler.CkeckIfJoystickPressed())
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
        InitialRotateAngle = Mathf.Atan2(_player.InputHandler.MovementPosX, _player.InputHandler.MovementPosY) * Mathf.Rad2Deg;
        if (RotateAngle < 0)
        {
            RotateAngle = -RotateAngle;
            gameObject.transform.rotation = Quaternion.Euler(0, 180, -(RotateAngle - initialAngle));
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -(RotateAngle - initialAngle));
        }
    }

    public void Accept(IVisitor visitor)
    {
        _player = gameObject.FindWithTag(RotatableWeaponData.PlayerTag).GetComponent<Player>();
        visitor.Visit(this);

        //_player.WeaponHandler.Weaponry.CurrentWeapon.Accept(rotatable)
    }
    public void UnsetPlayer()
    { 
        _player = null;
    }
}
