using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileOld : ProjectileOld
{
    private Transform _target;
    private float rotateSpeed = 200f;
    private float speed = 5;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Wall").transform;
    }

    private void Update()
    {

     
    }
    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)_target.position - rb.position;

        direction.Normalize();

        float randomIndex = Random.Range(0.01f, 1f);
        float rotateAmount = Vector3.Cross(direction, transform.right).z + randomIndex;

        rb.angularVelocity = - rotateAmount * rotateSpeed;


        rb.velocity = transform.right * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
      
    }

    public override void FireProjectile(float speed, Vector2 direction, float travelDistance, float lifeDuration, float dragMultiplier)
    {
        // smth
    }
}
