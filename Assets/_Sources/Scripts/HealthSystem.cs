using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float _health;
    [SerializeField] private float _maxHealth;

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
    public float GetCurrentHealth()
    {
        return _health;
    }

    public void DecreaseHealth(float damageAmount)
    {
        _health -= damageAmount;
        if (_health < 0)
        {
            _health = 0;
            Destroy(gameObject);
        }
    }

    public void IncreaseHealth(float healAmount)
    {
        _health += healAmount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

}
