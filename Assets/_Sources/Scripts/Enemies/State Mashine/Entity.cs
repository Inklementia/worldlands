using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMashine StateMachine;
    public D_Entity EntityData;

    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public GameObject AliveGO { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }
    public GameObject Player { get; private set; }
    public Vector2 StartingPos { get; private set; }


    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform playerCheck;
    //public Transform TestTarget;

    public int FacingDirection { get; private set; }
   
    private Vector2 _velocityWorkspace;

    public virtual void Start()
    {
        AliveGO = transform.Find("Alive").gameObject;
        Anim = AliveGO.GetComponent<Animator>();
        Rb = AliveGO.GetComponent<Rigidbody2D>();
        AnimationToStateMachine = AliveGO.GetComponent<AnimationToStateMachine>();

        StartingPos = transform.position;
        FacingDirection = -1;
        StateMachine = new FiniteStateMashine();

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(Vector2 direction, float speed)
    {
       //direction.Normalize();
        _velocityWorkspace = direction * speed;
        Rb.velocity = _velocityWorkspace;
    }

    public virtual void SetVelocityZero()
    {
        _velocityWorkspace = Vector2.zero;
        Rb.velocity = _velocityWorkspace;
    }


    public virtual void GoTo(Vector2 point, float speed)
    {
        var distance = Vector2.Distance(AliveGO.transform.position, point);
        Rb.DOMove(point, distance/speed);
        //transform.position = Vector2.MoveTowards(transform.position, MovePos, speed * Time.deltaTime);
    }


    public virtual void StopMovement()
    {
        Rb.DOPause();
    }

    public virtual bool CheckWall()
    {
        return
            //Physics2D.Raycast(wallCheck.position, AliveGO.transform.right, EntityData.WallCheckDistance, EntityData.WhatIsWall) ||
            //Physics2D.Raycast(wallCheck.position, -AliveGO.transform.right, EntityData.WallCheckDistance, EntityData.WhatIsWall) ||
            //Physics2D.Raycast(wallCheck.position, AliveGO.transform.up, EntityData.WallCheckDistance, EntityData.WhatIsWall) ||
            //Physics2D.Raycast(wallCheck.position, -AliveGO.transform.up, EntityData.WallCheckDistance, EntityData.WhatIsWall);
            Physics2D.CircleCast(wallCheck.position, EntityData.WallCheckDistance, AliveGO.transform.right, 0.1f, EntityData.WhatIsWall);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.CircleCast(playerCheck.position, EntityData.MinAgroDistance, AliveGO.transform.right, 0.1f, EntityData.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.CircleCast(playerCheck.position, EntityData.MaxAgroDistance, AliveGO.transform.right, 0.1f, EntityData.WhatIsPlayer);
    }

    //in melee attack Range
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.CircleCast(playerCheck.position, EntityData.CloseRangeActionDistance, AliveGO.transform.right, 0.1f, EntityData.WhatIsPlayer);
    }

    public virtual Vector2 GetPlayerPosition()
    {
        return Player.transform.position;
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGO.transform.Rotate(0, 180, 0);
    }

    public void Flip180()
    {
        FacingDirection = 1;
        AliveGO.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void Flip0()
    {
        FacingDirection = -1;
        AliveGO.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public virtual void OnDrawGizmos()
    {
        
        //Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDirection * EntityData.WallCheckDistance));
        //Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(-Vector2.right * FacingDirection * EntityData.WallCheckDistance));
        //Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.up * FacingDirection * EntityData.WallCheckDistance));
        //Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(-Vector2.up * FacingDirection * EntityData.WallCheckDistance));

        Gizmos.DrawWireSphere(wallCheck.position, EntityData.WallCheckDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerCheck.position, EntityData.MinAgroDistance);
        Gizmos.DrawWireSphere(playerCheck.position, EntityData.MaxAgroDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerCheck.position, EntityData.CloseRangeActionDistance);
  
        Gizmos.color = Color.white;


    }
}
