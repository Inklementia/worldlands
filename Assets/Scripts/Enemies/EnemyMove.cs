using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemyAction
{
 
    [SerializeField] private float speed = 3f;
    [SerializeField] private float waitForTurnTime;

    [SerializeField] private float patrolDistance = 10;
    [SerializeField] private float attackDistance = 2; // remove
    [SerializeField] private float chaseTriggerDistance = 3;


    private Vector2 _movePos;
    private Vector2 _originalPos;

    private float _waitTimer;
    private bool _animationFinished = true;
   

    private void Start()
    {
       
        _originalPos = transform.position;
        _waitTimer = waitForTurnTime;
        ChangeMoveDirection();
    }


    private void FixedUpdate()
    {
        if (!triggerChaseArea._isInTriggerChaseArea)
        {
            PatrolArea();
            HandleFlip();
        }
        else
        {
            Chase();
            HandleChaseFlip();
        }
        
    }

    private void HandleFlip()
    {
        if (_movePos.x < transform.position.x)
        {
            enemyToFlip.transform.rotation = Quaternion.identity;    
        }
        else if (_movePos.x > transform.position.x)
        {
            enemyToFlip.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void HandleChaseFlip()
    {

        if (_player.transform.position.x <= transform.position.x)
        {
            enemyToFlip.transform.rotation = Quaternion.identity;
        }
        else if (_player.transform.position.x > transform.position.x)
        {
            enemyToFlip.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    private void PatrolArea()
    {
    
        transform.position = Vector2.MoveTowards(transform.position, _movePos, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, _movePos) < 0.2f)
        {
            if(_waitTimer <= 0)
            {
                ChangeMoveDirection();
                _waitTimer = waitForTurnTime;

                anim.SetBool("run", true);
                anim.SetBool("idle", false);
            }
            else
            {
                _waitTimer -= Time.deltaTime;
                anim.SetBool("run", false);
                anim.SetBool("idle", true);
            }
        }
        
    }

    private void ChangeMoveDirection()
    {
        _movePos = new Vector2(
                 Random.Range(_originalPos.x - patrolDistance, _originalPos.x + patrolDistance),
                 Random.Range(_originalPos.y - patrolDistance, _originalPos.y + patrolDistance)
                 );

        //Debug.Log(_movePos.x + " , " + _movePos.y);
    }


    private void Chase()
    {
        if (!CheckIfShouldStopMoving() && _animationFinished)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
            anim.SetBool("run", true);
            anim.SetBool("idle", false);
            anim.SetBool("attack", false);
        }
        else if (triggerAttackArea._isInTriggerAttackArea)
        {
            anim.SetBool("attack", true);
            anim.SetBool("run", false);
        }

    }

    public void SetAnimationFinish()
    {
        _animationFinished = true;

    }

    public void ResetAnimationFinish()
    {
        _animationFinished = false;

    }

    public void Bite()
    {
        if(_player != null)
        {
            _player.GetComponent<HealthSystem>().Damage(10);
        }
        
    }
    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    private bool CheckIfShouldStopMoving()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < attackDistance)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    //public virtual bool CheckPlayerInMaxChaseRange()
    //{
    //    return Physics2D.Raycast(_player.transform.position, transform.right, chaseTriggerDistance, whatIsPlayer);
    //}

    private void OnDrawGizmos()
    {
  
        Gizmos.DrawWireSphere(_originalPos, patrolDistance);

        //Gizmos.color = Color.white;
        //Gizmos.DrawWireSphere(transform.position, chaseTriggerDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - attackDistance, transform.position.y));

    }

}
