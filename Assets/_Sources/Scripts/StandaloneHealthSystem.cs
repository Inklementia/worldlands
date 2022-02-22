using UnityEngine;

namespace _Sources.Scripts
{
    public class StandaloneHealthSystem : MonoBehaviour, IHealthSystem
    {
        public float Health { get; private set; }
        public float MaxHealth { get; private set; }
    

        public float GetCurrentHealth()
        {
            return Health;
        }
        public void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void DecreaseHealth(float damageAmount)
        {
            Health -= damageAmount;
            if (Health < 0)
            {
                Health = 0;
                //Destroy(gameObject);
            }
        }

        public void IncreaseHealth(float healAmount)
        {
            Health += healAmount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
    }
}