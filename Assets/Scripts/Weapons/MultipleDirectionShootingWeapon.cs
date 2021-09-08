using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class MultipleDirectionShootingWeapon : ShootingWeapon
    {
        private List<Vector2> _firePoints = new List<Vector2>();

       
        private float _projectileSpeed;
        private float _randomAngleDeviation;
        private float _randomProjectileSpeedDeviation;

        private bool _fullRound;

        //private float _halfAngle;
        //private int _numberOfSegments;

        private void Start()
        {  
            _projectileSpeed = WeaponDataStats.ProjectileSpeed;
          
            if (!IsRotatable)
            {
                AssignFirePoints();
            }

            Debug.Log("Rotate: " + IsRotatable);
            Debug.Log("Charge: " + ShouldBeCharged);
            Debug.Log("Animate: " + ShouldBeAnimatedBeforeAttack);
        }
        public override void Attack()
        {
            if (CanFire)
            {
     
                if (IsRotatable)
                {
                    AssignFirePoints();
                }

                if (CanFire && !ShouldBeCharged && !ShouldBeAnimatedBeforeAttack)
                {
                    CanFire = false;
                    FireInAssignedDirections();
                    
                }
                else if (CanFire && (ShouldBeCharged && Charged))
                {
                    CanFire = false;
                    Charged = false;
                    FireSingleRoundOfProjectilesInAssignedDirections();
                }
                else if (CanFire && (ShouldBeAnimatedBeforeAttack && Animated))
                {
                    Animated = false;
                    CanFire = false;
                    FireInAssignedDirections();
                }
            }
        }

        private void FireInAssignedDirections()
        {
            foreach (var point in _firePoints)
            {
                _randomAngleDeviation = Random.Range(-WeaponDataStats.AngleDeviation, WeaponDataStats.AngleDeviation);
                _randomProjectileSpeedDeviation = Random.Range(-WeaponDataStats.ProjectileSpeedDeviation, WeaponDataStats.ProjectileSpeedDeviation);

                var direction = new Vector3(point.x + _randomAngleDeviation, point.y + _randomAngleDeviation, 0);
                float angle = Mathf.Atan2(point.x + _randomAngleDeviation, point.y + _randomAngleDeviation) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        

                if (WeaponDataStats.NumberOfProjectilesPerRound > 1)
                {
                    StartCoroutine(SpawnProjectilesInInterval(
                        attackPosition.position, 
                        Quaternion.AngleAxis(90 - angle, Vector3.forward), 
                        direction,
                        _projectileSpeed + _randomProjectileSpeedDeviation,
                        WeaponDataStats.Damage,
                        WeaponDataStats.ProjectileRotationSpeed, 
                        WeaponDataStats.ProjectileRotateAngleDeviation));

                }
                else
                {
                    SpawnProjectile(
                        attackPosition.position, 
                        Quaternion.AngleAxis(90 - angle, Vector3.forward), 
                        direction,

                        _projectileSpeed + _randomProjectileSpeedDeviation,
                        WeaponDataStats.Damage,
                        WeaponDataStats.ProjectileRotationSpeed, 
                        WeaponDataStats.ProjectileRotateAngleDeviation);
                }
            }
        }

        private void FireSingleRoundOfProjectilesInAssignedDirections()
        {
            foreach (var point in _firePoints)
            {
                _randomAngleDeviation = Random.Range(-WeaponDataStats.AngleDeviation, WeaponDataStats.AngleDeviation);
                _randomProjectileSpeedDeviation = Random.Range(-WeaponDataStats.ProjectileSpeedDeviation, WeaponDataStats.ProjectileSpeedDeviation);

                var direction = new Vector3(point.x + _randomAngleDeviation, point.y + _randomAngleDeviation, 0);
                float angle = Mathf.Atan2(point.x + _randomAngleDeviation, point.y + _randomAngleDeviation) * Mathf.Rad2Deg;

                SpawnProjectile(
                    attackPosition.position, 
                    Quaternion.AngleAxis(90 - angle, Vector3.forward), 
                    direction,
                    _projectileSpeed + _randomProjectileSpeedDeviation,
                    WeaponDataStats.Damage,
                    WeaponDataStats.ProjectileRotationSpeed, 
                    WeaponDataStats.ProjectileRotateAngleDeviation);
                
            }
        }

        private void AssignFirePoints()
        {
            _firePoints.Clear();
            if (IsRotatable)
            {
                Angle = RotatableWeapon.InitialRotateAngle;
            }
            else
            {
                Angle = 90;
            }
            
            var halfAngle = Mathf.Ceil(WeaponDataStats.FireAngle / 2);
            var segments = WeaponDataStats.NumberOfProjectiles;
            if (WeaponDataStats.RemoveAdditionalSegmentAtEnd)
            {
                segments = WeaponDataStats.NumberOfProjectiles - 1;
            }

            var startAngle = Angle - halfAngle;
            var endAngle = Angle + halfAngle;

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
            var halfAngle = Mathf.Ceil(WeaponDataStats.FireAngle / 2);
            var segments = WeaponDataStats.NumberOfProjectiles;
            if (WeaponDataStats.RemoveAdditionalSegmentAtEnd)
            {
                segments = WeaponDataStats.NumberOfProjectiles - 1;
            }
            

            if (IsRotatable)
            {
                Angle = RotatableWeapon.InitialRotateAngle;
            }
            else
            {
                Angle = 90;
            }

            //var segments = numberOfBullets + 1;

            var startAngle = Angle - halfAngle;
            var endAngle = Angle + halfAngle;
            List<Vector2> arcPoints = new List<Vector2>();
            float angle = startAngle;
            float arcLength = endAngle - startAngle;
            for (int i = 0; i <= segments; i++)
            {

                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * 2;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * 2;

                arcPoints.Add(new Vector2(x, y));

                Gizmos.color = Color.white;
                Gizmos.DrawLine(attackPosition.position, new Vector2( attackPosition.position.x + x, attackPosition.position.y + y));

                angle += (arcLength / segments);
            }
        }
        }
    }
