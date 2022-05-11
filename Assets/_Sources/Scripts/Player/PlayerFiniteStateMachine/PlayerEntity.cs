using System;
using System.Collections.Generic;
using _Sources.Scripts.Core;
using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using _Sources.Scripts.Input;
using _Sources.Scripts.Player.Data;
using _Sources.Scripts.Player.PlayerStates;
using _Sources.Scripts.UI;
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
        public PlayerUI playerUI;
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
            Core.HealthSystem.IsDead = false;
            Core.HealthSystem.SetMaxStat(playerData.MaxHealth);
            Core.EnergySystem.SetMaxStat(playerData.MaxEnergy);
            
            if (ES3DataManager.Instance.Health != 0)
            {
                Core.HealthSystem.SetMaxStat(ES3DataManager.Instance.Health);
            }
           
            if (ES3DataManager.Instance.Energy != 0)
            {
               Core.HealthSystem.SetMaxStat(ES3DataManager.Instance.Energy + 20);
            }

            Core.ShieldSystem.SetMaxStat(playerData.MaxShield);

            //getting components
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            WeaponHandler = GetComponent<PlayerWeaponHandler>();

            StateMachine.Initialize(IdleState);
            
            playerUI.SetUI(this);
            //RightInputHandler.DisableWeaponSwitchButton();
        }


        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
        }
        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        public void CheckDamage()
        {
            if(Core.HealthSystem.IsDead)
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
  
