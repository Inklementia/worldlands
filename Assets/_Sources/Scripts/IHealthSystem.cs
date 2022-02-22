namespace _Sources.Scripts
{
    public interface IHealthSystem
    {
        float Health { get; }
        float MaxHealth { get; }
        float GetCurrentHealth();
        void SetMaxHealth(float maxHealth);
        void DecreaseHealth(float damageAmount);
        void IncreaseHealth(float healAmount);
    }
}