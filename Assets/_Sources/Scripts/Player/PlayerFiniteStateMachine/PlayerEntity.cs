using System;
using System.Collections.Generic;
using _Sources.Scripts.Core;
using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using _Sources.Scripts.Player.PlayerStates;
using UnityEngine;

namespace _Sources.Scripts.Player.PlayerFiniteStateMachine
{
    public class PlayerEntity : MonoBehaviour, ISavedProgress
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

        private List<Vector3> _currentMap; // ?

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
            Core.HealthSystem.SetMaxStat(playerData.MaxHealth);
            Core.EnergySystem.SetMaxStat(playerData.MaxEnergy);
            Core.ShieldSystem.SetMaxStat(playerData.MaxShield);

            //getting components
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            WeaponHandler = GetComponent<PlayerWeaponHandler>();

            StateMachine.Initialize(IdleState);

            //RightInputHandler.DisableWeaponSwitchButton();
        }

        private void OnEnable()
        {
            GameActions.Current.OnDungeonGeneratedToSaveMap += SaveCurrentMap;
        }

        private void OnDisable()
        {
            GameActions.Current.OnDungeonGeneratedToSaveMap -= SaveCurrentMap;
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
            Core.HealthSystem.DecreaseStat(attackDetails.DamageAmount);
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

            if(Core.HealthSystem.GetCurrentStat() <= 0)
            {
                StateMachine.ChangeState(DeadState);
            }
        }


        public void LoadProgress(PlayerProgress progress)
        {
            //progress.WorldData.PositionOnLevel = new PositionOnLevel(1,1,transform.position.AsVectorData(),)
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            throw new System.NotImplementedException();
        }

        private void SaveCurrentMap(List<Vector3> tiles)
        {
            _currentMap = tiles;
        }
    }
}
  
