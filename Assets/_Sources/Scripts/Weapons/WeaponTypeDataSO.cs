using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAdditionalWeaponData", menuName = "Data/Weapon Data/Additional Data")]
public class WeaponTypeDataSO : ScriptableObject, IVisitor
{
    [Header("Rotating")]
    public Tag PlayerTag; 

    [Header("Charging")]
    [Range(0, 5)]
    public float MaxCharge = 3f;
    [Range(0, 5)]
    public float MinCharge = .5f;
    public bool CanFireWithoutCharge = false;

    [Header("Multiple Direction Shooting")]
    public float FireAngle = 30;
    public int NumberOfProjectiles = 4;
    [Range(0, 3)]
    public float AngleDeviation;
    [Range(0, 3)]
    public float ProjectileSpeedDeviation;
    public bool RemoveAdditionalSegmentAtEnd;

    public void Visit(RotatableWeapon rotatableWeapon)
    {
        //Debug.Log("Chargeable part attached");
    }
    public void Visit(ChargeableWeapon chargeableWeapon)
    {
       //Debug.Log("Chargeable part attached");
    }
    public void Visit(MultishotWeapon multishotWeapon)
    {
        //Debug.Log("Chargeable part attached");
    }
}

