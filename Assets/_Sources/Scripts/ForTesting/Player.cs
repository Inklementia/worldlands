using System;
using System.Collections;
using System.Timers;
using UnityEngine;

namespace _Sources.Scripts.ForTesting
{
    
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed;
        private Vector2 _movementPos;
        private bool _isMoving;
        private void Update()
    {

        _movementPos.x = Input.GetAxisRaw("Horizontal");
        _movementPos.y = Input.GetAxisRaw("Vertical");

        //Debug.Log(touchField.TouchDist+", ("+gameObject.transform.position.x+", "+ gameObject.transform.position.y+")") ;

        _isMoving = _movementPos != Vector2.zero;
        
    }

    private void FixedUpdate()
    {
        rb.velocity = _movementPos * speed;
        HandleFlip();
    }

    
    private void HandleFlip()
    {
        if(_movementPos.x > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else if(_movementPos.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    } 
}

