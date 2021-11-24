using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private Tag StickTo;
    private Vector2 _direction;
    private float _speed;

    private void Start()
    {
        Rb.velocity = _direction * _speed;
    }


    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.HasTag(StickTo))
        {
            transform.parent = collision.gameObject.transform;
          
           // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Rb.velocity = Vector2.zero;
            Rb.gravityScale = 0;
           // transform.Rotate(0f, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f); 
            //transform.rotation = Quaternion.identity;
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

    }



}
