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
    public PlayerInputHandler InputHandler { get; private set; } // move to script resp for weapons
    public int FacingDirection { get; private set; }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerWeaponry weaponry;
    [SerializeField] private int weaponsToCarry = 1;

    private Vector2 _velocityWorkSpace;
    private EncounteredWeapon _encounteredWeapon;

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

        InputHandler.DisableWeaponSwitchButton();
    }
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();

        // remove from udpate (make call from ui buttons)
        if (InputHandler.IsSwitchWeaponButtonPressed)
        {
            weaponry.SwitchWeapon();
        }

        if (_encounteredWeapon.Weapon != null)
        {
           
            if (InputHandler.IsActionButtonPressed && weaponry.CarriedWeapons.Count < weaponsToCarry)
            {
                weaponry.EquipWeapon(_encounteredWeapon.Weapon);
                //InputHandler.EnableWeaponSwitchButton();
                _encounteredWeapon.Weapon = null;
                if (weaponry.CarriedWeapons.Count > 1)
                {
                    InputHandler.EnableWeaponSwitchButton();
                }
               
            }
            else if (InputHandler.IsActionButtonPressed && weaponry.CarriedWeapons.Count >= weaponsToCarry)
            {
                weaponry.DropCurrentWeapon(_encounteredWeapon.Position);
                weaponry.EquipWeapon(_encounteredWeapon.Weapon);
            }
            // otherwise drop current and pick-up new
        }
        if (InputHandler.IsActionButtonPressed)
        {
            Debug.Log("Attack");
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

    // TODO: change to Tags
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            _encounteredWeapon.Position = collision.transform;
            _encounteredWeapon.Weapon = collision.GetComponent<Weapon>();
           
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //InputHandler.DisableActionButton();
    }
}
  
