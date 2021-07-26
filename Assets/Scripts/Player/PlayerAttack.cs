using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerAction
{
    
    private Weapon _weapon;

    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        if (inputHandler._isAttackButtonPressed)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _weapon.Attack();
    }
}
