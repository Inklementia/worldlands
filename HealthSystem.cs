using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : CoreComponent
{
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public HealthSystem(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    //remove
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
}
