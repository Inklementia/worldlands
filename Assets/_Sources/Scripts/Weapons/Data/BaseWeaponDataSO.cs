using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBaseWeaponData", menuName = "Data/Weapon Data/Base Data")]
public class BaseWeaponDataSO : ScriptableObject
{
    public string Name;

    public float AttackCd = .2f;

    [Header("Projectile")]
    public float ProjectileTravelDistance = 5f;
    public float ProjectileLifeDuration = 6f;
    public float ProjectileDragMultiplier = .5f;
    public float ProjectileSpeed = 8f;
}
