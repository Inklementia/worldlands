
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ProjectileOld
{
    private float _speed;
    private Vector2 _direction;


    private void Start()
    {
        rb.velocity = _direction * _speed;
    }
    public override void FireProjectile(float speed, Vector2 direction, float travelDistance, float lifeDuration, float dragMultiplier)
    {
        _speed = speed;
        _direction = direction;
    }
}
