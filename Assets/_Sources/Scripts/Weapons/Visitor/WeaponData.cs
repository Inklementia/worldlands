using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestVisitor
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData")]
    public class WeaponData : ScriptableObject, IVisitor
    {
        public string powerupName;
        public GameObject powerupPrefab;
        public void Visit(MultishotWeapon bikeShield)
        {
            Debug.Log("THIS IS mULTISHOT WEAPON");
        }

        public void Visit(RotatableWeapon bikeWeapon)
        { 
            Debug.Log("THIS IS RotatableWeapon WEAPON");
        }

        public void Visit(ChargeableWeapon bikeEngine)
        {
            Debug.Log("THIS IS ChargeableWeapon WEAPON");
        }
    }
}
