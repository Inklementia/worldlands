using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerWeaponry currentWeaponry;
    [SerializeField] private int weaponsToCarry = 1;

    [SerializeField] private Tag WeaponTag;

    private EncounteredWeapon _encounteredWeapon;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler.ChangeActionModeOnPickUp();
    }

    // Update is called once per frame
    private void Update()
    {
        //do events
        if (currentWeaponry.CanSwitchWeapons)
        {
            inputHandler.EnableWeaponSwitch();
        }
        else
        {
            inputHandler.DisableWeaponSwitch();
        }

        if (inputHandler.IsSwitchWeaponButtonPressed)
        {
            currentWeaponry.SwitchWeapon();
        }

        if (_encounteredWeapon.Weapon != null)
        {
            inputHandler.ChangeActionModeOnPickUp();
            if (inputHandler.IsPickUpButtonPressed && currentWeaponry.CarriedWeapons.Count < weaponsToCarry)
            {
                currentWeaponry.EquipWeapon(_encounteredWeapon.Weapon);
                //InputHandler.EnableWeaponSwitchButton();
                _encounteredWeapon.Weapon = null;


            }
            else if (inputHandler.IsPickUpButtonPressed && currentWeaponry.CarriedWeapons.Count >= weaponsToCarry)
            {
                currentWeaponry.DropCurrentWeapon(_encounteredWeapon.Position);
                currentWeaponry.EquipWeapon(_encounteredWeapon.Weapon);
            }
            //otherwise drop current and pick - up new
        }
        else
        {
            inputHandler.ChangeActionModeOnAttack();
        }

        // ATTACK
        if (inputHandler.IsAttackButtonPressed)
        {
            currentWeaponry.CurrentWeapon.Attack();
        }
    }

    // TODO: change to Tags
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.HasTag(WeaponTag))
        {
            _encounteredWeapon.Position = collision.transform;
            _encounteredWeapon.Weapon = collision.GetComponent<PlayerWeapon>();


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //InputHandler.DisableActionButton();
    }
}
