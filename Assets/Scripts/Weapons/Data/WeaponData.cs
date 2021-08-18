using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Data/Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Base")]
    public float EnergyCostPerAttack;
    public float AttackCooldown;

    [Header("Shooting")]
    public int NumberOfProjectilesPerRound = 1;
    public float IntervalBetweenRounds = .5f;

    [Header("Multiple Direction Shooting")]
    public float FireAngle = 30;
    public int NumberOfProjectiles = 4;
    [Range(0, 3)]
    public float AngleDeviation;
    [Range(0,3)]
    public float ProjectileSpeedDeviation;

    [Header("Charging")]
    [Range(0, 5)]
    public float MaxCharge;

    [Header("Bullet Projectile")]
    public float ProjectileTravelDistance = 5f;
    public float ProjectileLifeDuration = 6f;
    public float ProjectileDragMultiplier = .5f;
    public float ProjectileSpeed = 8f;

    [Header("Homing Missile Projectile")]
    public float ProjectileRotationSpeed = 200f;
    [Range(0, 3)]
    public float ProjectileRotateAngleDeviation;
}
