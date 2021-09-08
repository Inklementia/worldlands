using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class ShootingWeapon : BaseWeapon
    {
        [SerializeField] protected GameObject projectilePrefab;

        private void Start()
        {
            CanFire = true;
        }

        private void Update()
        {
         
            // if attack button
            CheckIfCanFire();
        }

        //single bullet
        protected void SpawnProjectile(Vector3 position, Quaternion rotation, Vector2 direction, 
            float projectileSpeed, int projectileDamage, float projectileRotationSpeed, float projectileRotationAngleDeviation)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, position, rotation);
            Projectile projectile = projectileGO.GetComponent<Projectile>();

            projectile.FireProjectile(
                projectileDamage,
                projectileSpeed,
                projectileRotationSpeed,
                projectileRotationAngleDeviation,
                direction,
                WeaponDataStats.ProjectileTravelDistance,
                WeaponDataStats.ProjectileLifeDuration,
                WeaponDataStats.ProjectileDragMultiplier
                );
        }

        // several bullets in round
        protected IEnumerator SpawnProjectilesInInterval(Vector3 position, Quaternion rotation, Vector2 direction,
            float projectileSpeed, int projectileDamage, float projectileRotationSpeed, float projectileRotationAngleDeviation)
        {
            for (int i = 0; i < WeaponDataStats.NumberOfProjectilesPerRound; i++)
            {
                SpawnProjectile(position, rotation, direction, 
                    projectileSpeed, projectileDamage, projectileRotationSpeed, projectileRotationAngleDeviation);
                yield return new WaitForSeconds(WeaponDataStats.IntervalBetweenRounds);
            }
        }

 
    }
}
