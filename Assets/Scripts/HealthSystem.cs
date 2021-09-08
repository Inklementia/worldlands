using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int _health;
    [SerializeField] private int _maxHealth;

    public HealthSystem(int maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }

    //remove
    private void Start()
    {
        _health = _maxHealth;
    }
    public int GetCurrentHealth()
    {
        return _health;
    }

    public void Damage(int damageAmount)
    {
        _health -= damageAmount;
        if(_health < 0)
        {
            _health = 0;
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        _health += healAmount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
    
}
