using _Sources.Scripts.Battle;
using _Sources.Scripts.Core.Components;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Object_Pooler;
using _Sources.Scripts.Structs;
using UnityEngine;

namespace _Sources.Scripts.Weapons.Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D Rb;
        [SerializeField] protected Tag targetTag;
        [SerializeField] protected Tag damagePopUpTag;
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

        protected void PlayDestroyParticles(ParticleSystem prefab, Vector3 position)
        {
            if (prefab == null)
            {
                return;
            }

            var instance = Instantiate(prefab, position, Quaternion.identity);
            instance.Play();
            var main = instance.main;
            Destroy(instance.gameObject, main.duration + main.startLifetime.constantMax);
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.HasTag(targetTag))
            {
                AttackDetails.Position = collision.transform.position;
                
                GameObject popUp = ObjectPooler.SpawnFromPool(damagePopUpTag, AttackDetails.Position, Quaternion.identity);
                popUp.transform.SetParent(collision.transform);
                popUp.GetComponent<DamagePopUp>().SetUp(DamageAmount);
                // decreasing health if projectile health target, and Tag matches target Tag

                if (collision.GetComponentInParent<Entity>() != null)
                {
                  
                    collision.GetComponentInParent<Entity>().Damage(AttackDetails);
                }
                else
                {
                 
                    collision.GetComponent<EnemySpawner>().TakeDamage(AttackDetails);
                }
                
                // turn off projectile
              

            }
        }

    }
}
