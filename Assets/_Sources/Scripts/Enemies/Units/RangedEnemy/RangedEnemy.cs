using _Sources.Scripts.Enemies.Data;
using _Sources.Scripts.Enemies.State_Mashine;
using UnityEngine;

namespace _Sources.Scripts.Enemies.Units.RangedEnemy
{
    public class RangedEnemy : Entity
    {
        public RangedEnemy_IdleState IdleState { get; private set; }
        public RangedEnemy_MoveState MoveState { get; private set; }
        public RangedEnemy_PlayerDetectedState PlayerDetectedState { get; private set; }
        public RangedEnemy_MeleeAttackState MeleeAttackState { get; private set; }
        public RangedEnemy_RangedAttackState RangedAttackState { get; private set; }
        public RangedEnemy_RunFromPlayerState RunFromPlayerState { get; private set; }
        public RangedEnemy_DamageState DamageState { get; private set; }
        public RangedEnemy_DeadState DeadState { get; private set; }
        
        [SerializeField] private D_IdleState idleStateData;
        [SerializeField] private D_MoveState moveStateData;
        [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
        [SerializeField] private D_MeleeAttackState meleeAttackStateData;
        [SerializeField] private D_RangedAttackState rangedAttackStateData;
        [SerializeField] private D_DamageState damageStateData;
        [SerializeField] private D_DeadState deadStateData;

        [SerializeField] private bool canMelee;
        [SerializeField] private Transform meleeAttackPosition;
        [SerializeField] private Transform rangedAttackPosition;

        public override void Start()
        {
            base.Awake();

            IdleState = new RangedEnemy_IdleState(this, StateMachine, "idle", idleStateData, this);
            MoveState = new RangedEnemy_MoveState(this, StateMachine, "move", moveStateData, this);
            PlayerDetectedState = new RangedEnemy_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedStateData, this);
            MeleeAttackState = new RangedEnemy_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
            RangedAttackState = new RangedEnemy_RangedAttackState(this, StateMachine, "rangeAttack", rangedAttackPosition, rangedAttackStateData, this);
            RunFromPlayerState = new RangedEnemy_RunFromPlayerState(this, StateMachine, "move", moveStateData, this);
            DamageState = new RangedEnemy_DamageState(this, StateMachine,"damage", damageStateData, this);
            DeadState = new RangedEnemy_DeadState(this, StateMachine, "dead", deadStateData, this);

            StateMachine.Initialize(MoveState);
        }
        
        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        

            Gizmos.color = Color.red;
            if (meleeAttackPosition != null)
            {
                Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.AttackRadius);
            }
            
            
        }

        public override void TakeDamage(AttackDetails attackDetails)
        {
            base.TakeDamage(attackDetails);
        
               
            if(IsDead)
            {
                StateMachine.ChangeState(DeadState);
            }
            else
            {
                StateMachine.ChangeState(DamageState);
            }
  
        }
    }
}