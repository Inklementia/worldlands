using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

public class MeleeEnemy_PatrolState : PatrolState
{
    private MeleeEnemy _enemy;
    public MeleeEnemy_PatrolState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, D_PatrolState stateData, MeleeEnemy enemy) : 
        base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        ChangeMoveDirection();
        HandleFlip();
        //_enemy.GoTo(_enemy._path.vectorPath[_enemy._currentWayPoint], StateData.MovementSpeed);
        //_enemy.GoTo(_enemy.MoveTarget.position, StateData.MovementSpeed);
        //Debug.Log("Current Waypoint + "+_enemy._path.vectorPath[_enemy._currentWayPoint]);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        //if (IsPlayerInMinAgroRange)
        //{
        //    StateMachine.ChangeState(_enemy.PlayerDetectedState);
        //}
        //else if (IsDetectingWall || Vector2.Distance(_enemy.transform.position, MovePos) < 0.2f || _enemy._reachedEndOfPath)
        //{
        //    StateMachine.ChangeState(_enemy.IdleState);
        //}
     
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();


        //if (_enemy._path == null)
        //{
        //    Debug.Log("Pathh null");
        //    return;
        //}
        //if (_enemy._currentWayPoint >= _enemy._path.vectorPath.Count)
        //{
        //    Debug.Log("reached end TRUE");
        //    _enemy._reachedEndOfPath = true;
        //    return;
        //}
        //else
        //{
        //    Debug.Log("reached end False");
        //    _enemy._reachedEndOfPath = false;
        //}
        //Vector2 direction = ((Vector2)_enemy._path.vectorPath[_enemy._currentWayPoint] - _enemy.Rb.position).normalized;
        ////Vector2 force = direction * StateData.MovementSpeed * Time.deltaTime;

        //_enemy.Rb.velocity = direction * StateData.MovementSpeed * Time.deltaTime; 

        //float distance = Vector2.Distance(_enemy.Rb.position, _enemy._path.vectorPath[_enemy._currentWayPoint]);
        ////Debug.Log("Distance " + distance);
        //if (distance < _enemy.NextWayPointDistance)
        //{
        //    _enemy._currentWayPoint++;
        //}
    }


}
