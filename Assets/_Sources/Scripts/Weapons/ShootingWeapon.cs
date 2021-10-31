using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWeapon : PlayerWeapon
{
    [SerializeField] private ProjectileDataSO weaponData;
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

    //protected void SpawnProjectile(Vector3 position, Quaternion rotation, Vector2 direction,
    //float projectileSpeed, float projectileRotationSpeed, float projectileRotationAngleDeviation)
    //{
    //    GameObject projectileGO = Instantiate(projectilePrefab, position, rotation);
    //    Projectile projectile = projectileGO.GetComponent<Projectile>();

    //    projectile.FireProjectile(
    //        projectileSpeed,
    //        projectileRotationSpeed,
    //        projectileRotationAngleDeviation,
    //        direction,
    //        BaseWeaponData.ProjectileTravelDistance,
    //        BaseWeaponData.ProjectileLifeDuration,
    //        BaseWeaponData.ProjectileDragMultiplier
    //        );
    //}

    //// several bullets in round
    //protected IEnumerator SpawnProjectilesInInterval(Vector3 position, Quaternion rotation, Vector2 direction,
    //    float projectileSpeed, float projectileRotationSpeed, float projectileRotationAngleDeviation)
    //{
    //    for (int i = 0; i < BaseWeaponData.NumberOfProjectilesPerRound; i++)
    //    {
    //        SpawnProjectile(position, rotation, direction,
    //            projectileSpeed, projectileRotationSpeed, projectileRotationAngleDeviation);
    //        yield return new WaitForSeconds(BaseWeaponData.IntervalBetweenRounds);
    //    }
    //}

}
