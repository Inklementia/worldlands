using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected string targetTag = "Enemy";

        protected GameObject[] _targets;
        protected int _damage;

        public virtual void FireProjectile(
            int damage,
            float speed,
            float rotationSpeed,
            float rotationAngleDeviation,
            Vector2 direction,
            float travelDistance,
            float lifeDuration,
            float dragMultiplier
            )
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == targetTag)
            {
                // TODO: instaciate particles
                
                collision.gameObject.GetComponentInParent<HealthSystem>().Damage(_damage);
                gameObject.SetActive(false);

            }
        }
    }   
}
