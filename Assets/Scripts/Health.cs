using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private int currentHealth = 100;

    void Start()
    {
        Debug.Log("My starting health is: " + currentHealth);
    }

    //void OnEnable()
    //{
    //    Damageable.OnDamage += DamageHealth;
    //}
    //void OnDisable()
    //{
    //    Damageable.OnDamage -= DamageHealth;
    //}
    public void DamageHealth(int damageAmount)
    {
        currentHealth = currentHealth - damageAmount;
        Debug.Log("My health is: " + currentHealth);
    }
}
