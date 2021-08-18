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

        private float _lifeDuration;

        private float _lifeDurationTimer = 0.0f;

 
        public abstract void FireProjectile(
            float speed,
            float rotationSpeed,
            float rotationAngleDeviation,
            Vector2 direction,
            float travelDistance,
            float lifeDuration,
            float dragMultiplier
            );


        private void FixedUpdate()
        {
            _lifeDurationTimer += Time.deltaTime;

            if (_lifeDurationTimer >= _lifeDuration)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == targetTag)
            {
                // TODO: instaciate particles
                gameObject.SetActive(false);

            }
        }
    }   
}
