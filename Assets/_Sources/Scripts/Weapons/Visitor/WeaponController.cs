using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestVisitor
{
public class WeaponController : MonoBehaviour, IWeaponElement
{
        private List<IWeaponElement> _weaponElements =
               new List<IWeaponElement>();

        void Start()
        {
            _weaponElements.Add(
                gameObject.AddComponent<RotatableWeapon>());
            _weaponElements.Add(
                gameObject.AddComponent<ChargeableWeapon>());
            _weaponElements.Add(
                gameObject.AddComponent<MultishotWeapon>());
        }

        public void Accept(IVisitor visitor)
        {
            foreach (IWeaponElement element in _weaponElements)
            {
                element.Accept(visitor);
            }
        }
    }
}


