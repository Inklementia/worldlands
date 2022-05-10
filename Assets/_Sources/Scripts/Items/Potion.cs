using _Sources.Scripts.Interfaces;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace _Sources.Scripts.Items
{
    public class Potion : ICollectable
    {
        [SerializeField] private float amount = 100;
        [SerializeField] private PotionType type;
        
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
    }
}