using UnityEngine;

namespace _Sources.Scripts.Weapons.Projectiles
{
    public class Rocket : BaseProjectile
    {
        private Transform _target;
        [SerializeField] protected ParticleSystem destroyParticlesPrefab;
        private ShootingWeapon _weapon;
        [SerializeField] private Tag DestroyOnto;

        private float _speed;
        private float _rotationSpeed;
        private float _rotateAngleDeviation;

        private float _randomRotateAngleDeviation;

        private void FixedUpdate()
        {
            if (_weapon == null)
            {
                return;
            }

            var closestEnemy = _weapon.RotatableWeapon?.Aimhelper?.ClosestEnemy;
            if (closestEnemy != null)
            {
                _target = closestEnemy.transform;
                Vector2 direction = (Vector2)_target.position - Rb.position;

                direction.Normalize();

                _randomRotateAngleDeviation = Random.Range(-_rotateAngleDeviation, _rotateAngleDeviation);
                float rotateAmount = Vector3.Cross(direction, transform.right).z + _randomRotateAngleDeviation;

                Rb.angularVelocity = -rotateAmount * _rotationSpeed;
            }

            Rb.velocity = transform.right * _speed;
        }

        public override void FireProjectile(
            float damage,
            float speed,
            float rotationSpeed,
            float rotationAngleDeviation,
            Vector2 direction,
            float travelDistance,
            float lifeDuration,
            float dragMultiplier,
            ShootingWeapon weapon
        )
        {
            DamageAmount = damage;
            _speed = speed;
            _rotationSpeed = rotationSpeed;
            _rotateAngleDeviation = rotationAngleDeviation;
      
            _weapon = weapon;
            AttackDetails.DamageAmount = damage;
            AttackDetails.Position = transform.position;
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (collision.HasTag(DestroyOnto))
            {
                var position = transform.position;
                PlayDestroyParticles(destroyParticlesPrefab, position);
           
                //objectPooler.SpawnFromPool(splashTag, position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                gameObject.SetActive(false);

            }
        }
    }
}
