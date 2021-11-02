using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestDecorator
{
    public class ClientDecorator : MonoBehaviour
    {
        private PlayerWeapon _playereWeapon;
        private bool _isWeaponDecorated;

        void Start()
        {
            _playereWeapon =
                (PlayerWeapon)
                FindObjectOfType(typeof(PlayerWeapon));
        }

        void OnGUI()
        {
            if (!_isWeaponDecorated)
                if (GUILayout.Button("Decorate Weapon"))
                {
                    _playereWeapon.Decorate();
                    _isWeaponDecorated = !_isWeaponDecorated;
                }

            if (_isWeaponDecorated)
                if (GUILayout.Button("Reset Weapon"))
                {
                    _playereWeapon.Reset();
                    _isWeaponDecorated = !_isWeaponDecorated;
                }

            if (GUILayout.Button("Toggle Fire"))
                _playereWeapon.ToggleFire();
        }
    }
}