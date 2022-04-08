using System;
using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Weapons;
using _Sources.Scripts.Weapons.Projectiles;
using MoreMountains.Feedbacks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlowDownProjectile : BaseProjectile
{
    [SerializeField] private Tag DestroyOnto;
    [SerializeField] protected MMFeedback destroyParticlesFeedback;
    private Vector2 _direction;

    private float _travelDistance;
    private float _dragMultiplier;
    private float _speed;
    private float _xStartPos;
    private float _maxDrag = 30;
    private float _lifeDuration;
    private float _lifeDurationTimer = 0.0f;

    private bool _shouldSlowDown;

    private void OnEnable()
    {

        _lifeDurationTimer = 0.0f;
        _shouldSlowDown = false;

        Rb.drag = 0;
        _xStartPos = transform.position.x;
    }


    private void FixedUpdate()
    {
        Rb.velocity = _direction * _speed;

        CheckDisable();
        //CheckSlowDown();
    }

    private void CheckDisable()
    {
        _lifeDurationTimer += Time.deltaTime;

        if (_lifeDurationTimer >= _lifeDuration)
        {
            gameObject.SetActive(false);
        }
    }
    private void CheckSlowDown()
    {
        if (Mathf.Abs(_xStartPos - transform.position.x) >= _travelDistance && !_shouldSlowDown)
        {
            _shouldSlowDown = true;
            //rb.velocity = rb.velocity / Time.deltaTime;
            StartCoroutine(AddDrag());
        }
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

        AttackDetails.DamageAmount = damage;
        AttackDetails.Position = transform.position;
        
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
            Rb.drag = current_drag;
            yield return null;
        }

        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = 0f;
        Rb.drag = 0;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.HasTag(DestroyOnto))
        {
            // TODO: instantiate particles
            var position = transform.position;
            destroyParticlesFeedback.Play(position, 1);
         
            //objectPooler.SpawnFromPool(splashTag, position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            gameObject.SetActive(false);

        }
    }
}

