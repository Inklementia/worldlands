using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Sources.Scripts.Weapons
{
    public class PlayerWeaponry : MonoBehaviour
    {
        private int _totalWeapons;
        private int _currentWeaponIndex;
        public List<ShootingWeapon> CarriedWeapons { get; private set; }
        public ShootingWeapon CurrentWeapon { get; private set; }
        public bool CanSwitchWeapons { get; private set; }

        private void Start()
        {
            CountCarriedWeapons();
            _currentWeaponIndex = 0;
            CanSwitchWeapons = false;
        }

        private void Update()
        {
            //Debug.Log("Switch: "+CanSwitchWeapons);
        }

        private void CountCarriedWeapons()
        {

            CarriedWeapons = GetComponentsInChildren<ShootingWeapon>().ToList();
            _totalWeapons = CarriedWeapons.Count;

   
            //EquipWeapon(CarriedWeapons[_currentWeaponIndex]);
        }
        public void EquipWeapon(ShootingWeapon newWeapon)
        {
            // hide current weapon
            if (CarriedWeapons.Count > 0)
            {
                foreach (ShootingWeapon Weapon in CarriedWeapons)
                {
                    Weapon.UnEquip();
                }
            }
            // add new weapon 
            CurrentWeapon = newWeapon;
            CarriedWeapons.Add(CurrentWeapon);
            _totalWeapons++;
            _currentWeaponIndex++;

            // if more than 2 weapons
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

        private void PickUpWeapon()
        {
            CurrentWeapon.Equip();
Debug.Log("PickUp "+CurrentWeapon.name);
            CurrentWeapon.gameObject.transform.SetParent(transform);
            CurrentWeapon.gameObject.transform.localRotation = Quaternion.identity;
            CurrentWeapon.gameObject.transform.localPosition = Vector3.zero;
            CurrentWeapon.gameObject.transform.localScale = Vector3.one;
        }

        public void DropCurrentWeapon(Transform dropTransform)
        {
            CurrentWeapon.gameObject.transform.parent = null;
            CurrentWeapon.gameObject.transform.position = dropTransform.position;
            CurrentWeapon.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            CarriedWeapons.Remove(CurrentWeapon);
            _totalWeapons--;
            CurrentWeapon.RemovePlayerInteractions();

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
}
