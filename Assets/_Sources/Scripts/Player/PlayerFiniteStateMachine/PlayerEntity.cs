using _Sources.Scripts.Core;
using _Sources.Scripts.Player.PlayerStates;
using UnityEngine;

namespace _Sources.Scripts.Player.PlayerFiniteStateMachine
{
    public class PlayerEntity : MonoBehaviour
    {
        #region States
        public PlayerCore Core { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerWeaponHandler WeaponHandler { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerDamageState DamageState { get; private set; }
        public PlayerDeadState DeadState { get; private set; }
        #endregion

        public Animator Anim { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }

        [SerializeField] private PlayerDataSO playerData;
   
        private void Awake()
        {
        
            Core = GetComponentInChildren<PlayerCore>();
            StateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
            DamageState = new PlayerDamageState(this, StateMachine, playerData, "damage");
            DeadState = new PlayerDeadState(this, StateMachine, playerData, "dead");
            //AttackState = new PlayerAttackState(this, StateMachine, playerData);
        }
        private void Start()
        {
            // core initial funtions
        
            Core.Movement.SetFacingDirection(1);
            Core.HealthSystem.SetMaxHealth(playerData.MaxHealth);

            //getting components
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            WeaponHandler = GetComponent<PlayerWeaponHandler>();

            StateMachine.Initialize(IdleState);

            //RightInputHandler.DisableWeaponSwitchButton();
        }
        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();  

        }
        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        public virtual void Damage(AttackDetails attackDetails)
        {
            Debug.Log("damaged " + attackDetails.DamageAmount);
            Core.HealthSystem.DecreaseHealth(attackDetails.DamageAmount);
            StateMachine.ChangeState(DamageState);

            if(attackDetails.Position.x > transform.position.x)
            {
                // _lastDamageDirection = -1;
            }
            else
            {
                //_lastDamageDirection = 1;
            }

            //_knockbackStartTime = Time.time;

            //Core.Movement.SetVelocity(EntityData.KnockBackAngle, EntityData.KnockBackSpeed, _lastDamageDirection);

            if(Core.HealthSystem.GetCurrentHealth() <= 0)
            {
                StateMachine.ChangeState(DeadState);
            }
        }
        
     
    }
}
  
