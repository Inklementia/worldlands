using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerWeaponry Weaponry { get; private set; }

    [SerializeField] private float weaponsNumberToCarry = 2;
    [SerializeField] private Tag WeaponTag;

    private EncounteredWeapon _encounteredWeapon;

    private void Awake()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Weaponry = GetComponentInChildren<PlayerWeaponry>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InputHandler.ChangeActionModeOnPickUp();
    }

    // Update is called once per frame
    private void Update()
    {
        //_encounteredWeapon = Player.Core.CollisionSenses.EncounteredWeapon;

        //do events

        ManageSwitchingAndEquipingWeapons();
        ManageAttacking();


    }
    private void ManageSwitchingAndEquipingWeapons()
    {
        // if more than 1 weapons
        if (Weaponry.CanSwitchWeapons)
        {
            InputHandler.EnableWeaponSwitch();
        }
        else
        {
            InputHandler.DisableWeaponSwitch();
        }
        // switching 
        if (InputHandler.IsSwitchWeaponButtonPressed)
        {
            Weaponry.SwitchWeapon();
        }

        // if player is near weapon
        if (_encounteredWeapon.Weapon != null)
        {
            // activate Action button
            InputHandler.ChangeActionModeOnPickUp();

            // if Action button is pressed and Player can take 1 more weapon
            if (InputHandler.IsPickUpButtonPressed && Weaponry.CarriedWeapons.Count < weaponsNumberToCarry)
            {
                Weaponry.EquipWeapon(_encounteredWeapon.Weapon);
                //InputHandler.EnableWeaponSwitchButton();
                _encounteredWeapon.Weapon = null;
            }
            else if (InputHandler.IsPickUpButtonPressed && Weaponry.CarriedWeapons.Count >= weaponsNumberToCarry)
            {
                Weaponry.DropCurrentWeapon(_encounteredWeapon.Position);
                Weaponry.EquipWeapon(_encounteredWeapon.Weapon);
            }
            //otherwise drop current and pick - up new
        }
        else
        {
            //StartCoroutine(WaitAndChangeActionModeOnAttack());
            InputHandler.ChangeActionModeOnAttack();
        }
    }
    private void ManageAttacking()
    {
        // if player has weapon
        if (Weaponry.CurrentWeapon != null)
        {
            if(InputHandler.IsAttackButtonPressed && !Weaponry.CurrentWeapon.ShouldBeCharged)
            {
                Weaponry.CurrentWeapon.Attack();
            }
            else if (InputHandler.IsAttackButtonPressed && Weaponry.CurrentWeapon.ShouldBeCharged)
            {
                // charging
                Weaponry.CurrentWeapon.ChargeableWeapon.Charge();
            }
            else if(!InputHandler.IsAttackButtonPressed && (Weaponry.CurrentWeapon.ShouldBeCharged && Weaponry.CurrentWeapon.Charged))
            {
                Weaponry.CurrentWeapon.Attack();
            }
        }

    }
    //����� ��� ��, ����� �������� ������ �� ����������� �����
    private IEnumerator WaitAndChangeActionModeOnAttack()
    {
        yield return new WaitForSeconds(.5f);
        InputHandler.ChangeActionModeOnAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool once = true;
        if (collision.HasTag(WeaponTag) && once)
        {
            _encounteredWeapon.Position = collision.transform;
            _encounteredWeapon.Weapon = collision.GetComponent<ShootingWeapon>();
            once = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.HasTag(WeaponTag))
        {
            _encounteredWeapon.Weapon = null;
        }
    }
}