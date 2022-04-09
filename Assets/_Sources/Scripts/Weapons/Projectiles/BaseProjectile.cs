using _Sources.Scripts.Core.Components;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

namespace _Sources.Scripts.Weapons.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D Rb;
        [SerializeField] protected Tag TargetTag;

        protected float DamageAmount;
        protected AttackDetails AttackDetails;

        protected ObjectPooler ObjectPooler;

        private void Start()
        {
            ObjectPooler = ObjectPooler.Instance;
        }

        // upon firing a projectile assign all needed info about its behaviur
        public virtual void FireProjectile(
            float damageAmount,
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
            DamageAmount = damageAmount;
            AttackDetails.DamageAmount = damageAmount;
            AttackDetails.Position = transform.position;
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.HasTag(TargetTag))
            {
                // decreasing health if projectile health target, and Tag matches target Tag
               
               
              collision.GetComponentInParent<Entity>().Damage(AttackDetails);
                // turn off projectile
                gameObject.SetActive(false);

            }
        }

    }
}
