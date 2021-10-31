using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    [Header("Wall")]
    [SerializeField] private Transform wallCheck;

    [SerializeField] private float wallCheckDistance = 1f;

    [SerializeField] private LayerMask whatIsWall;

    [Header("Player")]
    [SerializeField] private Transform playerCheck;

    [SerializeField] private float maxAgroDistance = 4f; //  i. e. for chase / detect
    [SerializeField] private float minAgroDistance = 1f; // for attacking

    [SerializeField] private float closeRangeActionDistance = 1f;

    [SerializeField] private LayerMask whatIsPlayer;

    public virtual bool CheckIfPlayerInMinAgroRange()
    {
        return Physics2D.CircleCast(playerCheck.position, minAgroDistance, transform.right, 0.1f, whatIsPlayer);
    }

    public virtual bool CheckIfPlayerInMaxAgroRange()
    {
       return Physics2D.CircleCast(playerCheck.position, maxAgroDistance, transform.right, 0.1f, whatIsPlayer);
    }

    //in melee attack Range
    public virtual bool CheckIfPlayerInCloseRangeAction()
    {
       return Physics2D.CircleCast(playerCheck.position, closeRangeActionDistance, transform.right, 0.1f, whatIsPlayer);
    }

    public virtual bool CheckWall()
    {
        return Physics2D.CircleCast(wallCheck.position, wallCheckDistance, transform.right, 0.1f, whatIsWall);
    }

    protected virtual void OnDrawGizmos()
    {
        if(Core != null)
        {
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerCheck.position, minAgroDistance);
            Gizmos.DrawWireSphere(playerCheck.position, maxAgroDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(playerCheck.position, closeRangeActionDistance);

            Gizmos.color = Color.white;
        }
    

    }
}
