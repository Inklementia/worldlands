using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Arrow : Projectile
    {
        private float _speed;
        private Vector2 _direction;


        public override void FireProjectile(
            float speed, 
            float rotationSpeed, 
            float rotationAngleDeviation, 
            Vector2 direction, 
            float travelDistance, 
            float lifeDuration, 
            float dragMultiplier)
        {
            _speed = speed;
            _direction = direction;
        }

        void Start()
        {
            rb.velocity = _direction * _speed;
        }

      
    }
}

