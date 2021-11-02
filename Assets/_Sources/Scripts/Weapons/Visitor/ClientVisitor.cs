using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestVisitor
{
    public class ClientVisitor : MonoBehaviour
    {
        public WeaponData rotatable;
        public WeaponData chargeable;
        public WeaponData multishot;

        private WeaponController _weaponController;

        void Start()
        {
            _weaponController =
                gameObject.
                    AddComponent<WeaponController>();
        }

        void OnGUI()
        {
            if (GUILayout.Button("Weapon rotatable"))
                _weaponController.Accept(rotatable);

            if (GUILayout.Button("Weapon chargeable"))
                _weaponController.Accept(chargeable);

            if (GUILayout.Button("Weapon multishot"))
                _weaponController.Accept(multishot);
        }
    }
}

