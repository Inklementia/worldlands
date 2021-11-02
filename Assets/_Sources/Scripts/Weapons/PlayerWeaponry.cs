using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeaponry : MonoBehaviour
{
    private int _totalWeapons;
    private int _currentWeaponIndex;
    public List<Weapon> CarriedWeapons { get; private set; }
    public Weapon CurrentWeapon { get; private set; }
    public bool CanSwitchWeapons { get; private set; }

    private void Start()
    {
        CountCarriedWeapons();
        _currentWeaponIndex = 0;
        CanSwitchWeapons = false;
    }

    private void CountCarriedWeapons()
    {

        CarriedWeapons = GetComponentsInChildren<Weapon>().ToList();
        _totalWeapons = CarriedWeapons.Count;

   
        //EquipWeapon(CarriedWeapons[_currentWeaponIndex]);
    }
    public void EquipWeapon(Weapon newWeapon)
    {

        if (CarriedWeapons.Count > 0)
        {
            foreach (Weapon Weapon in CarriedWeapons)
            {
                Weapon.UnEquip();
            }
        }
       
        CurrentWeapon = newWeapon;
        CarriedWeapons.Add(CurrentWeapon);
        _totalWeapons++;
        _currentWeaponIndex++;
        if (_totalWeapons > 1)
        {
            CanSwitchWeapons = true;
        }
        else
        {
            CanSwitchWeapons = false;
        }
        PickUpWeapon();

        //CountCarriedWeapons();
    }
    public void PickUpWeapon()
    {
        CurrentWeapon.Equip();

        CurrentWeapon.gameObject.transform.parent = transform;
        CurrentWeapon.gameObject.transform.localRotation = Quaternion.identity;
        CurrentWeapon.gameObject.transform.localPosition = Vector3.zero;
        CurrentWeapon.gameObject.transform.localScale = Vector3.one;
    }

    public void DropCurrentWeapon(Transform dropTransform)
    {
        CurrentWeapon.gameObject.transform.parent = null;
       CurrentWeapon.gameObject.transform.position = dropTransform.position;
        CarriedWeapons.Remove(CurrentWeapon);
        _totalWeapons--;

    }
    public void SwitchWeapon()
    {
        CurrentWeapon.UnEquip();

        if (_currentWeaponIndex < _totalWeapons - 1)
        {
            _currentWeaponIndex += 1;

        }
        else if (_currentWeaponIndex >= _totalWeapons - 1)
        {
            _currentWeaponIndex = 0;
        }

        CurrentWeapon = CarriedWeapons[_currentWeaponIndex];
        CurrentWeapon.Equip();

    }
}
