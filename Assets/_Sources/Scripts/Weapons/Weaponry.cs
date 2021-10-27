using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponry : MonoBehaviour
{
    public Weapon[] CarriedWeapons { get; private set; }
    public Weapon CurrentWeapon { get; private set; }

    private void Start()
    { 
        CarriedWeapons = GetComponentsInChildren<Weapon>();
        EquipWeapon(CarriedWeapons[0]);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        foreach (Weapon Weapon in CarriedWeapons)
        {
            Weapon.UnEquip();
        }
        CurrentWeapon = newWeapon;
        CurrentWeapon.Equip();
    }

    public void ChangeWeapon(Weapon anotherWeapon)
    {
        CurrentWeapon.UnEquip();
        CurrentWeapon = anotherWeapon;
        CurrentWeapon.Equip();
    }
    public void SwitchWeapon()
    {
        var currentIndex = Array.IndexOf(CarriedWeapons, CurrentWeapon);

        if(currentIndex == 1)
        {
            currentIndex = 0;
        }
        else if (currentIndex == 0)
        {
            currentIndex = 1;
        }

        CurrentWeapon.UnEquip();
        CurrentWeapon = CarriedWeapons[currentIndex];
        CurrentWeapon.Equip();
    }
}
