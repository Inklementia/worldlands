using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private Transform scopePos;
    [SerializeField] private int NumberOfProjectilesPerAttack;
    [SerializeField] private int EnergyCostPerAttack;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;
    [SerializeField] private float randomness;

    public override void Attack()
    {
        Instantiate(projectile, scopePos.position, scopePos.rotation);
    }
}