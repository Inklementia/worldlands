using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Enemies.States;

public class MeleeEnemy_SpawnState : SpawnState
{
    private MeleeEnemy _enemy;
    
   
   

    public MeleeEnemy_SpawnState(Entity entity, FiniteStateMashine stateMachine, string animBoolName, MeleeEnemy enemy) : 
        base(entity, stateMachine, animBoolName)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (IsAnimationFinished)
        {
            if (!PerformCloseRangeAction)
            {
                if (IsPlayerInMinAgroRange)
                {
                    StateMachine.ChangeState(_enemy.PlayerDetectedState);
                }
                else
                {
                    StateMachine.ChangeState(_enemy.IdleState);
                }
            }
         
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}