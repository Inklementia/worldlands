using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBaseProjectileData", menuName = "Data/Projectile Data/Base Data")]
public class ProjectileDataSO : ScriptableObject
{
    public float ProjectileTravelDistance = 5f;
    public float ProjectileLifeDuration = 6f;
    public float ProjectileDragMultiplier = .5f;
    public float ProjectileSpeed = 8f;
}
