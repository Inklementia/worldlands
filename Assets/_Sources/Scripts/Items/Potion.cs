using System;
using _Sources.Scripts.Interfaces;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.Items
{
    public class Potion : MonoBehaviour, ICollectable
    {
        [SerializeField] private float amount = 100;
        [SerializeField] private PotionType type;
        [SerializeField] private Tag playerTag;
        public void ApplyTo(PlayerEntity playerEntity)
        {
            if (type == PotionType.Health)
            {
                playerEntity.Core.HealthSystem.IncreaseStat(amount);
            }
            else
            {
                playerEntity.Core.EnergySystem.IncreaseStat(amount);
            }
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.HasTag(playerTag))
            {
                //animation
                ApplyTo(col.GetComponent<PlayerEntity>());
                gameObject.SetActive(false);
            }
        }
    }
    
}