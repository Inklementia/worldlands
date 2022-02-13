using System;
using System.Collections.Generic;
using System.Linq;
using _Sources.Scripts.Enemies.State_Mashine;
using Dungeon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        //[SerializeField] private ItemFrequency weapons;
        [SerializeField] private List<GameObject> weapons = new List<GameObject>();

        /*
        private void OnEnable()
        {
            Entity.OnDrop += DropWeapon;
        }

        private void OnDisable()
        {
            Entity.OnDrop += DropWeapon;
        }
*/
        public void DropWeapon(Transform place)
        {
            if (weapons.Count > 0)
            {
                int randomIndex = Random.Range(0, weapons.Count);
                var weapon = weapons[randomIndex];
                Instantiate(weapon, place.position, Quaternion.identity);
                weapons.Remove(weapon);
            }
          
        }
    }
}