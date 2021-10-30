using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected BaseWeaponData BaseWeaponData;

    protected Transform AttackPosition;

    public bool CanFire { get; protected set; }
    private float _coolDownTimer = 0.0f;


    public void Equip()
    {
        gameObject.SetActive(true);
    }
    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    // cooldown
    public void CheckIfCanFire()
    {
        if (CanFire == false)
        {
            _coolDownTimer += Time.deltaTime;
            if (_coolDownTimer >= BaseWeaponData.AttackCd)
            {
                CanFire = true;
                _coolDownTimer = 0.0f;
            }
        }
    }

    public abstract void Attack();
}
