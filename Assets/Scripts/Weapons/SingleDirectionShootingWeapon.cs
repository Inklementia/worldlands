using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons 
{
    public class SingleDirectionShootingWeapon : ShootingWeapon
    {

        public override void Attack()
        {
            if (CanFire && !ShouldBeCharged)
            {
                Fire();
                CanFire = false;
            }
            else if (CanFire && (ShouldBeCharged && Charged))
            {
                CanFire = false;
                Charged = false;
                SpawnProjectile(attackPosition.position, attackPosition.rotation, transform.right,
                  ChargebleWeapon.ChargedProjectileSpeed, WeaponDataStats.Damage, WeaponDataStats.ProjectileRotationSpeed, WeaponDataStats.ProjectileRotateAngleDeviation);
             

            }
        }

        private void Fire()
        {
            if (WeaponDataStats.NumberOfProjectilesPerRound > 1)
            {
                StartCoroutine(SpawnProjectilesInInterval(attackPosition.position, attackPosition.rotation, transform.right,
                    WeaponDataStats.ProjectileSpeed, WeaponDataStats.Damage, WeaponDataStats.ProjectileRotationSpeed, WeaponDataStats.ProjectileRotateAngleDeviation));
            }
            else
            {
                SpawnProjectile(attackPosition.position, attackPosition.rotation, transform.right,
                    WeaponDataStats.ProjectileSpeed, WeaponDataStats.Damage, WeaponDataStats.ProjectileRotationSpeed, WeaponDataStats.ProjectileRotateAngleDeviation);
            }
        }
    }
}
