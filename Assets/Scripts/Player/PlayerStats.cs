using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxShield = 100f;

    private float _healthCapacity;
    private float _shieldCapacity;

    private void Start()
    {
        _healthCapacity = maxHealth;
        _shieldCapacity = maxShield;
    }

    
}
