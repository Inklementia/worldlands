using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Infrastructure;
using _Sources.Scripts.Services.Input;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region States
    public Core Core { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerWeaponHandler WeaponHandler { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; } 
    
    
    
    [SerializeField] private PlayerDataSO playerData;

    private void Awake()
    {
        
        Core = GetComponentInChildren<Core>();
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
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
            gameObject.SetActive(false);
        }
    }
}
  
