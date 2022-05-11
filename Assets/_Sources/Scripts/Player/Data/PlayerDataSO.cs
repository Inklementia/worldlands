using UnityEngine;

namespace _Sources.Scripts.Player.Data
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
    public class PlayerDataSO : ScriptableObject
    {
        public float MaxHealth = 100;
        public float MaxEnergy = 100;
        public float MaxShield = 100;
        public float ShieldRegenSpeed = 10;

        [Header("Move State")]
        public float MovementVelocity = 4f;

        [Header("Damage State")]
        public float DamageTime = .5f;

        public Tag HitParticlesTag;
    


    }
}
