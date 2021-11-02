using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Feature")]
public class AdditionalWeaponFeatureDataSO : ScriptableObject, IVisitor
{
    [Header("Charging")]
    [Range(0, 5)]
    public float MaxCharge;

    [Header("Bullet Projectile")]
    public float ProjectileTravelDistance = 5f;
    public float ProjectileLifeDuration = 6f;
    public float ProjectileDragMultiplier = .5f;
    public float ProjectileSpeed = 8f;

    public void Visit(RotatableWeapon rotatableWeapon)
    {
        Debug.Log("Proof that multishotWeapon");
    }
    public void Visit(ChargeableWeapon chargeableWeapon)
    {
        Debug.Log("Proof that multishotWeapon");
    }
    public void Visit(MultishotWeapon multishotWeapon)
    {
        Debug.Log("Proof that multishotWeapon");
    }
}

