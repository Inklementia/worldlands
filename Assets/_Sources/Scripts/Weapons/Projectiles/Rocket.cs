using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Sources.Scripts.Core.Components;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Weapons;
using _Sources.Scripts.Weapons.Projectiles;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Rocket : BaseProjectile
{
    private Transform _target;

    private ShootingWeapon _weapon;
    //[SerializeField] protected MMFeedback destroyParticlesFeedback;
    [SerializeField] private Tag DestroyOnto;
    //[SerializeField] private Tag splashTag;
    //[SerializeField] protected MMFeedback destroyParticlesFeedback;

    private float _speed;
    private float _rotationSpeed;
    private float _rotateAngleDeviation;

    private float _randomRotateAngleDeviation;
    

    private void FixedUpdate()
    {
        _target = _weapon.RotatableWeapon.Aimhelper.ClosestEnemy.transform;
        Vector2 direction = (Vector2)_target.position - Rb.position;

        direction.Normalize();

        _randomRotateAngleDeviation = Random.Range(-_rotateAngleDeviation, _rotateAngleDeviation);
        float rotateAmount = Vector3.Cross(direction, transform.right).z + _randomRotateAngleDeviation;

        Rb.angularVelocity = -rotateAmount * _rotationSpeed;
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
            // TODO: instantiate particles

           // destroyParticlesFeedback.Play(position, 1);
           
            //objectPooler.SpawnFromPool(splashTag, position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            gameObject.SetActive(false);

        }
    
        
    }
}

