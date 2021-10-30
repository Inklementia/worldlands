using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDirectionalShootingWeapon : PlayerWeapon
{
   //[SerializeField] private SingleDirectionalShootingWeaponData weaponData;
    [SerializeField] private GameObject projectilePrefab;

    private void Start()
    {
        CanFire = true;
    }

    private void Update()
    {

        // if attack button
        CheckIfCanFire();
    }
    public override void Attack()
    {
        if (CanFire)
        {
            Debug.Log("FIRE");
            CanFire = false;
        }
      
    }
}
