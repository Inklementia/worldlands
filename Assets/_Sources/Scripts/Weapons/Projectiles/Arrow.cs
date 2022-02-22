using System;
using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Weapons.Projectiles;
using UnityEngine;

public class Arrow : BaseProjectile
{
    [SerializeField] private Tag StickTo;
    private Vector2 _direction;
    private float _speed;


    private void Awake()
    {
       
    }

    
    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.HasTag(StickTo))
        {
            transform.parent = collision.gameObject.transform;

            Rb.velocity = Vector2.zero;
            Rb.gravityScale = 0;
     
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
        float dragMultiplier)
    {
        DamageAmount = damage;

        _speed = speed;
        _direction = direction;
        
        Rb.velocity = _direction * _speed;
        Rb.gravityScale = 1;

    }



}
