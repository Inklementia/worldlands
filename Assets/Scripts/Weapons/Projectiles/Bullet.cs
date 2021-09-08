using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Bullet : Projectile
    {
        private float _travelDistance;
        private float _dragMultiplier;

        private float _speed;
        private Vector2 _direction;


        private bool _shouldSlowDown;
        private float _xStartPos;
        private float _maxDrag = 30;

        private float _lifeDuration;

        private float _lifeDurationTimer = 0.0f;

        private void Start()
        {
            _shouldSlowDown = false;

            rb.drag = 0;
            _xStartPos = transform.position.x;

            rb.velocity = _direction * _speed; 
        }


        private void FixedUpdate()
        {
            _lifeDurationTimer += Time.deltaTime;

            if (_lifeDurationTimer >= _lifeDuration)
            {
                gameObject.SetActive(false);
            }

            if (Mathf.Abs(_xStartPos - transform.position.x) >= _travelDistance && !_shouldSlowDown)
            {
                _shouldSlowDown = true;
                //rb.velocity = rb.velocity / Time.deltaTime;
                StartCoroutine(AddDrag());
            }
        }

        public override void FireProjectile(
            int damage,
            float speed,
            float rotationSpeed,
            float rotationAngleDeviation,
            Vector2 direction,
            float travelDistance,
            float lifeDuration,
            float dragMultiplier)
        {
            _damage = damage;
            _speed = speed;
            _direction = direction;
            _travelDistance = travelDistance;
            _lifeDuration = lifeDuration;
            _dragMultiplier = dragMultiplier;
        }


        private IEnumerator AddDrag()
        {
            float current_drag = 0;
            while (current_drag < _maxDrag)
            {
                current_drag += Time.deltaTime * _dragMultiplier;
                rb.drag = current_drag;
                yield return null;
            }

            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.drag = 0;
        }
    }

}
