using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Weapons.Projectiles;
using _Sources.Scripts.Weapons.Weapon_Features;
using UnityEngine;

namespace _Sources.Scripts.Weapons
{
    public class ShootingWeapon : MonoBehaviour
    {
        [SerializeField] private BaseWeaponDataSO baseWeaponData;
     
        [SerializeField] private Transform attackPosition;
        [SerializeField] private Tag projectileTag;
        //[SerializeField] private Tag shootPartcilesTag;

        //[Header("Additional weapon features")]
        public BaseWeaponDataSO BaseWeaponData { get => baseWeaponData; private set => baseWeaponData = value; }
        public Transform AttackPosition { get => attackPosition; private set => attackPosition = value; }
        public RotatableWeapon RotatableWeapon { get; private set; }
        public ChargeableWeapon ChargeableWeapon { get; private set; }
        public MultishotWeapon MultishotWeapon { get; private set; }

        private List<IWeaponFeature> _weaponFeatures = new List<IWeaponFeature>();
        public bool CanFire { get; protected set; } 
        public bool Charged { get; protected set; }
        public bool ShouldBeCharged { get; protected set; }
        public bool IsRotatable { get; protected set; }
        public bool IsMultishoot { get; private set; }
        public float Angle { get; protected set; }

        private float _coolDownTimer = 0.0f;


        private float _projectileSpeed;
        private float _randomAngleDeviation;
        private float _randomProjectileSpeedDeviation;
        private ObjectPooler _pooler;

        private void Awake()
        {
            RotatableWeapon = gameObject.GetComponent<RotatableWeapon>();
            ChargeableWeapon = gameObject.GetComponent<ChargeableWeapon>();
            MultishotWeapon = gameObject.GetComponent<MultishotWeapon>();

            _weaponFeatures.Add(RotatableWeapon);
            _weaponFeatures.Add(ChargeableWeapon);
            _weaponFeatures.Add(MultishotWeapon);

            // bool to check what type of weapon we have 
            ShouldBeCharged = ChargeableWeapon != null ? true : false;
            IsRotatable = RotatableWeapon != null ? true : false;
            IsMultishoot = MultishotWeapon != null ? true : false;

        
        }

        private void Start()
        {
            _pooler = ObjectPooler.Instance;
            _projectileSpeed = baseWeaponData.ProjectileSpeed;

            if (!IsRotatable && MultishotWeapon != null)
            {
                MultishotWeapon.AssignFirePoints();
            }
        }

        private void Update()
        {
            CheckIfCanFire();
       
        }
        private void FixedUpdate()
        {
     
        }
        public void Accept(IVisitor visitor)
        {
            //foreach (IWeaponFeature element in _weaponFeatures)
            //{
            //    element.Accept(visitor);
            //}
            if(RotatableWeapon != null)
            {
                RotatableWeapon.Accept(visitor);
            }
            if (ChargeableWeapon != null)
            { 
                ChargeableWeapon.Accept(visitor);
            }
            if (MultishotWeapon != null)
            {
                MultishotWeapon.Accept(visitor);
            }

       
        }
        public void Equip()
        {
            gameObject.SetActive(true);

            if (RotatableWeapon != null)
            {
                Accept(RotatableWeapon.RotatableWeaponData);
            }
            if (ChargeableWeapon != null)
            {
                Accept(ChargeableWeapon.ChargeableWeaponData);
            }
            if (MultishotWeapon != null)
            {
                Accept(MultishotWeapon.MultishotWeaponData);
            }
            //Accept(visitor);

        }
        public void UnEquip()
        {
            gameObject.SetActive(false);

      
        }
        public void RemovePlayerInteractions()
        {
            if (RotatableWeapon != null)
            {
                RotatableWeapon.UnsetPlayer();
            }
            if (ChargeableWeapon != null)
            {
                ChargeableWeapon.UnsetPlayer();
            }
        }

        // cooldown
        private void CheckIfCanFire()
        {
            if (CanFire == false)
            {
                _coolDownTimer += Time.deltaTime;
                if (_coolDownTimer >= baseWeaponData.AttackCd)
                {
                    CanFire = true;
                    _coolDownTimer = 0.0f;
                }
            }
        }
        public void SetCharged()
        {
            Charged = true;
        }
        public void ResetCharged()
        {
            Charged = false;
        }
        public virtual void Attack()
        {
            if (CanFire)
            {
                if (IsRotatable && IsMultishoot)
                {
                    MultishotWeapon.AssignFirePoints();
                }

                if (CanFire && !ShouldBeCharged)
                {
                    if (!IsMultishoot)
                    {
                        FireInSingleDirection();
                        CanFire = false;
                    }
                    else
                    {
                        CanFire = false;
                        FireInMultipleDirections();
                    }

               
                }
                else if (CanFire && (ShouldBeCharged && Charged))
                {
                    CanFire = false;
                    Charged = false;

                    if (!IsMultishoot)
                    {
                        SpawnProjectile(
                            attackPosition.position, 
                            attackPosition.rotation,
                            transform.right,
                            ChargeableWeapon.ChargedProjectileSpeed,
                            baseWeaponData.Damage,
                            baseWeaponData.ProjectileRotationSpeed, 
                            baseWeaponData.ProjectileRotateAngleDeviation
                        );
                    }
                    else
                    {
                        FireSingleRoundOfProjectilesInAssignedDirections();
                    }

                }
            }  
        }

        private void FireInSingleDirection()
        {
            if (baseWeaponData.NumberOfProjectilesPerRound > 1)
            {
                StartCoroutine(
                    SpawnProjectilesInInterval(
                        attackPosition.position,
                        attackPosition.rotation,
                        transform.right,
                        baseWeaponData.ProjectileSpeed,
                        baseWeaponData.Damage,
                        baseWeaponData.ProjectileRotationSpeed,
                        baseWeaponData.ProjectileRotateAngleDeviation
                    )
                );
            }
            else
            {
                SpawnProjectile(
                    attackPosition.position, 
                    attackPosition.rotation, 
                    transform.right,
                    baseWeaponData.ProjectileSpeed, 
                    baseWeaponData.Damage, 
                    baseWeaponData.ProjectileRotationSpeed, 
                    baseWeaponData.ProjectileRotateAngleDeviation
                );
            }
        }

        private void FireInMultipleDirections()
        {
            foreach (var point in MultishotWeapon.FirePoints)
            {
                _randomAngleDeviation = Random.Range(-MultishotWeapon.MultishotWeaponData.AngleDeviation, MultishotWeapon.MultishotWeaponData.AngleDeviation);
                _randomProjectileSpeedDeviation = Random.Range(-MultishotWeapon.MultishotWeaponData.ProjectileSpeedDeviation, MultishotWeapon.MultishotWeaponData.ProjectileSpeedDeviation);

                var direction = new Vector3(point.x + _randomAngleDeviation, point.y + _randomAngleDeviation, 0);
                float angle = Mathf.Atan2(point.x + _randomAngleDeviation, point.y + _randomAngleDeviation) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


                if (baseWeaponData.NumberOfProjectilesPerRound > 1)
                {
                    StartCoroutine(SpawnProjectilesInInterval(
                        attackPosition.position,
                        Quaternion.AngleAxis(90 - angle, Vector3.forward),
                        direction,
                        _projectileSpeed + _randomProjectileSpeedDeviation,
                        baseWeaponData.Damage,
                        baseWeaponData.ProjectileRotationSpeed,
                        baseWeaponData.ProjectileRotateAngleDeviation));

                }
                else
                {
                    SpawnProjectile(
                        attackPosition.position,
                        Quaternion.AngleAxis(90 - angle, Vector3.forward),
                        direction,
                        _projectileSpeed + _randomProjectileSpeedDeviation,
                        baseWeaponData.Damage,
                        baseWeaponData.ProjectileRotationSpeed,
                        baseWeaponData.ProjectileRotateAngleDeviation);
                }
            }
        }
        // charge
        private void FireSingleRoundOfProjectilesInAssignedDirections()
        {
            foreach (var point in MultishotWeapon.FirePoints)
            {
                _randomAngleDeviation = Random.Range(-MultishotWeapon.MultishotWeaponData.AngleDeviation, MultishotWeapon.MultishotWeaponData.AngleDeviation);
                _randomProjectileSpeedDeviation = Random.Range(-MultishotWeapon.MultishotWeaponData.ProjectileSpeedDeviation, MultishotWeapon.MultishotWeaponData.ProjectileSpeedDeviation);

                var direction = new Vector3(point.x + _randomAngleDeviation, point.y + _randomAngleDeviation, 0);
                float angle = Mathf.Atan2(point.x + _randomAngleDeviation, point.y + _randomAngleDeviation) * Mathf.Rad2Deg;

                SpawnProjectile(
                    attackPosition.position,
                    Quaternion.AngleAxis(90 - angle, Vector3.forward),
                    direction,
                    ChargeableWeapon.ChargedProjectileSpeed + _randomProjectileSpeedDeviation,
                    baseWeaponData.Damage,
                    baseWeaponData.ProjectileRotationSpeed,
                    baseWeaponData.ProjectileRotateAngleDeviation);

            }
        }


        //single bullet
        protected void SpawnProjectile(Vector3 position, Quaternion rotation, Vector2 direction,
            float projectileSpeed, float projectileDamage, float projectileRotationSpeed, float projectileRotationAngleDeviation)
        {
            // SPAWN PARTICLES
        
            GameObject projectileGo = _pooler.SpawnFromPool(projectileTag, position, rotation);
        
            BaseProjectile baseProjectile = projectileGo.GetComponent<BaseProjectile>();

            baseProjectile.FireProjectile(
                projectileDamage,
                projectileSpeed,
                projectileRotationSpeed,
                projectileRotationAngleDeviation,
                direction,
                baseWeaponData.ProjectileTravelDistance,
                baseWeaponData.ProjectileLifeDuration,
                baseWeaponData.ProjectileDragMultiplier
            );
        }

        // several bullets in round
        protected IEnumerator SpawnProjectilesInInterval(Vector3 position, Quaternion rotation, Vector2 direction,
            float projectileSpeed, float projectileDamage, float projectileRotationSpeed, float projectileRotationAngleDeviation)
        {
            for (int i = 0; i < baseWeaponData.NumberOfProjectilesPerRound; i++)
            {
                SpawnProjectile(position, rotation, direction,
                    projectileSpeed, projectileDamage, projectileRotationSpeed, projectileRotationAngleDeviation);
                yield return new WaitForSeconds(baseWeaponData.IntervalBetweenRounds);
            }
        }
    }
}
