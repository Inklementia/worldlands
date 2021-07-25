using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float spawnDustInterval;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [SerializeField] private Joystick joystick;
    [SerializeField] private FixedTouchField touchField;

    [SerializeField] private GameObject weaponGO;
    [SerializeField] private GameObject dustGO;
    [SerializeField] private Animator dustAnim;
    [SerializeField] private Transform dustSpawnPoint;

    private Vector2 _movementPos;
    //private int _facingDirection = 1;
    private bool _isMoving;

    private float rotateAngle;

    private float dustTimer = 0.0f;

    private Weapon _weapon;


    private void Awake()
    {
        _weapon = weaponGO.GetComponent<Weapon>();
    }
    private void Update()
    {

        _movementPos.x = joystick.Horizontal;
        _movementPos.y = joystick.Vertical;

        //Debug.Log(touchField.TouchDist+", ("+gameObject.transform.position.x+", "+ gameObject.transform.position.y+")") ;

        _isMoving = _movementPos != Vector2.zero;

        anim.SetBool("run", _isMoving);

      
        if (_isMoving)
        {
            SpawnDust();
        }
        else
        {
            //check if dustGO is active
            if (dustGO.activeSelf)
            {
                dustAnim.SetTrigger("stop");
            }
          
            //RemoveDust();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = _movementPos * speed;
     
        if (touchField.Pressed)
        {
            HandleFlip();
            RotateWeapon();
        }
    }

    private void SpawnDust()
    {
        dustTimer += Time.deltaTime;

        if (!dustGO.activeSelf && dustTimer >= spawnDustInterval)
        {
            dustGO.transform.position = new Vector2(
              dustSpawnPoint.transform.position.x - (joystick.Direction.x),
              dustSpawnPoint.transform.position.y - (joystick.Direction.y)
            );

            dustTimer = 0.0f;
            dustGO.SetActive(true);
        }
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

    private void RotateWeapon()
    {
        rotateAngle = Mathf.Atan2(_movementPos.x, _movementPos.y) * Mathf.Rad2Deg;
        if (rotateAngle < 0)
        {
            rotateAngle = -rotateAngle;
            weaponGO.transform.rotation = Quaternion.Euler(0, 180, -(rotateAngle - 90));
        }
        else
        {
            weaponGO.transform.rotation = Quaternion.Euler(0, 0, -(rotateAngle - 90));
        } 
    }

    private void Attack()
    {
        _weapon.Attack();
    }
}
