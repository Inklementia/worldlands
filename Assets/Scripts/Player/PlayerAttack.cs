using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class PlayerAttack : PlayerAction
{
    private BaseWeapon _weapon;

    protected override void Awake()
    {
        base.Awake();
        _weapon = GetComponentInChildren<BaseWeapon>();
    }

    private void Update()
    {
        if ((inputHandler._isAttackButtonPressed && !_weapon.ShouldBeCharged && !_weapon.ShouldBeAnimatedBeforeAttack && !_weapon.IsAnimating) ||
            (!inputHandler._isAttackButtonPressed && (_weapon.ShouldBeCharged && _weapon.Charged)) ||
            (_weapon.ShouldBeAnimatedBeforeAttack && _weapon.Animated))

        {
            
            _weapon.Attack();
        }
        else if (inputHandler._isAttackButtonPressed && _weapon.ShouldBeCharged)
        {
            _weapon.ChargebleWeapon.Charge();
        }
        else if (inputHandler._isAttackButtonPressed && _weapon.ShouldBeAnimatedBeforeAttack && !_weapon.IsAnimating)
        {
            _weapon.AnimateBeforeAttackWeapon.AnimateAttack();
            Debug.Log(1);
        }
   
    }

}
