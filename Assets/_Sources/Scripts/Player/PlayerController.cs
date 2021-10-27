using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private HealthSystem health;

    [SerializeField] private Transform AttackPosition;
    [SerializeField] private float AttackRadius;
    [SerializeField] private LayerMask WhatAreEnemies;

    [SerializeField] private Transform enemy;

    private AttackDetails AttackDetails;

    private Vector2 _movement;
    private bool _isFacingRight = true;
    private bool _isRunning;

    private void Start()
    {
        
    }
    private void Update()
    {
        CheckAttackInput();
        CheckInput();
        CheckMovementDirection();
        UpdateAnimation();


     
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_movement.x, _movement.y) * speed;
     
  
    }

    private void CheckInput()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
    }
    private void CheckMovementDirection()
    {
        if(_isFacingRight && _movement.x < 0)
        {
            Flip();
        }
        else if(!_isFacingRight && _movement.x > 0)
        {
            Flip();
        }

        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }

    private void UpdateAnimation()
    {
        anim.SetBool("run", _isRunning);
    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    private void Damage(AttackDetails attackDetails)
    {
        health.DecreaseHealth(attackDetails.DamageAmount);

        // direction bullshit
    }

    private void CheckAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }
    private void Attack()
    {

        anim.SetTrigger("attack");
        //enemy.transform.SendMessage("Damage", AttackDetails);

    }
    public void TriggerAttack()
    {
        AttackDetails.DamageAmount = 100;
        AttackDetails.Position = transform.position;

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position, AttackRadius, WhatAreEnemies);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", AttackDetails);
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AttackPosition.position, AttackRadius);
    }
}
