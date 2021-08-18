using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class PlayerAttack : PlayerAction
{
    private BaseWeapon _weapon;

    private void Awake()
    {
        _weapon = GetComponentInChildren<BaseWeapon>();
    }

    private void Update()
    {
        if (inputHandler._isAttackButtonPressed && !_weapon.ShouldBeCharged)
        {
            _weapon.Attack();
        }
        else if(inputHandler._isAttackButtonPressed && _weapon.ShouldBeCharged)
        {
            Debug.Log("Should be charged");
            _weapon.ChargebleWeapon.Charge();
        }
        else if (!inputHandler._isAttackButtonPressed && (_weapon.ShouldBeCharged && _weapon.Charged))
        {
            Debug.Log("Should attcked");
            _weapon.Attack();
        }
    }

    private void Attack()
    {
        _weapon.Attack();
    }
}
