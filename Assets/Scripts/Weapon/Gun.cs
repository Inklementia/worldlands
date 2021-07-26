using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private Transform scopePos;
    [SerializeField] private int NumberOfProjectilesPerAttack;
    [SerializeField] private int EnergyCostPerAttack;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;
    [SerializeField] private float coolDown;

    private float _coolDownTimer = 0.0f;
    private bool _canFire = true;


    private void Update()
    {
        CheckIfCanFire();
    }
    public void CheckIfCanFire()
    {
        if(_canFire == false)
        {
            _coolDownTimer += Time.deltaTime;
            if(_coolDownTimer >= coolDown )
            {
                _canFire = true;
                _coolDownTimer = 0.0f;
            }
        }
    }

    public override void Attack()
    {
        if (_canFire)
        {
            Instantiate(projectile, scopePos.position, scopePos.rotation);
            _canFire = false;
        }
       
    }
}