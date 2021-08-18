using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class MultipleDirectionShootingWeapon : ShootingWeapon
    {
        private List<Vector2> _firePoints = new List<Vector2>();

       
        private float projectileSpeed;
        private float randomAngleDeviation;
        private float randomProjectileSpeedDeviation;

        //private float _halfAngle;
        //private int _numberOfSegments;

        private void Start()
        {
            AssignFirePoints();
            projectileSpeed = BaseWeaponData.ProjectileSpeed;
        }
        public override void Attack()
        {
            if (CanFire)
            {
                foreach (var point in _firePoints)
                {
                    randomAngleDeviation = Random.Range(-BaseWeaponData.AngleDeviation, BaseWeaponData.AngleDeviation);
                    randomProjectileSpeedDeviation = Random.Range(-BaseWeaponData.ProjectileSpeedDeviation, BaseWeaponData.ProjectileSpeedDeviation);

                    var direction = new Vector2(point.x + randomAngleDeviation, point.y + randomAngleDeviation);

                    if (BaseWeaponData.NumberOfProjectilesPerRound > 1)
                    {
                        StartCoroutine(SpawnProjectilesInInterval(attackPosition.position, attackPosition.rotation, direction, 
                            projectileSpeed + randomProjectileSpeedDeviation, BaseWeaponData.ProjectileRotationSpeed, BaseWeaponData.ProjectileRotateAngleDeviation));
    
                    }
                    else
                    {
                        SpawnProjectile(attackPosition.position, attackPosition.rotation, direction, 
                            projectileSpeed + randomProjectileSpeedDeviation, BaseWeaponData.ProjectileRotationSpeed, BaseWeaponData.ProjectileRotateAngleDeviation);
                    }
                }
                CanFire = false;
            }

        }

        private void AssignFirePoints()
        {
            var halfAngle = Mathf.Ceil(BaseWeaponData.FireAngle / 2);
            var segments = BaseWeaponData.NumberOfProjectiles - 1;

            var startAngle = 90 - halfAngle;
            var endAngle = 90 + halfAngle;

            float angle = startAngle;
            float arcLength = endAngle - startAngle;
            for (int i = 0; i <= segments; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle);
                float y = Mathf.Cos(Mathf.Deg2Rad * angle);

                _firePoints.Add(new Vector2(x, y));

                angle += (arcLength / segments);
            }
        }
        

        private void OnDrawGizmos()
        {
            // 30
            // 15
            var halfAngle = Mathf.Ceil(BaseWeaponData.FireAngle / 2);
            var segments = BaseWeaponData.NumberOfProjectiles - 1;
            //var segments = numberOfBullets + 1;

            var startAngle = 90 - halfAngle;
            var endAngle = 90 + halfAngle;
            List<Vector2> arcPoints = new List<Vector2>();
            float angle = startAngle;
            float arcLength = endAngle - startAngle;
            for (int i = 0; i <= segments; i++)
            {

                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * 2;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * 2;

                arcPoints.Add(new Vector2(x, y));

                Gizmos.color = Color.white;
                Gizmos.DrawLine(attackPosition.position, new Vector2(attackPosition.position.x + x, attackPosition.position.y + y));

                angle += (arcLength / segments);
            }
        }
        }
    }
