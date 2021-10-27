using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    #endregion

    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public int FacingDirection { get; private set; }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private Weaponry weaponry;

    private Vector2 _velocityWorkSpace;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        AttackState = new PlayerAttackState(this, StateMachine, playerData);
    }
    private void Start()
    {
        FacingDirection = 1;

        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        if (InputHandler.IsSwitchWeaponButtonPressed)
        {
            weaponry.SwitchWeapon();
        }
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocity(float velocityX, float velocityY)
    {
        _velocityWorkSpace.Set(velocityX, velocityY);
        Rb.velocity = _velocityWorkSpace;
    }
    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }
}
