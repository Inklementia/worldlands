using UnityEngine;

namespace _Sources.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "newBaseWeaponData", menuName = "Data/Weapon Data/Base Data")]
    public class BaseWeaponDataSO : ScriptableObject
    {
        public string Name;
        [Header("Base")]
        public float Damage = 10;
        public float EnergyCostPerAttack = 1f;
        public float AttackCd = .2f;

        [Header("Shooting")]
        public int NumberOfProjectilesPerRound = 1;
        public float IntervalBetweenRounds = .5f;

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
}
