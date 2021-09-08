using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{

    public EnemyMove enemyMove;

    // Update is called once per frame
    public void AllowMovement()
    {
        enemyMove.SetAnimationFinish();
 
    }

    public void DisallowMovement()
    {
        enemyMove.ResetAnimationFinish();
    }

    public void Attack()
    {
        enemyMove.Bite();
    }
}
