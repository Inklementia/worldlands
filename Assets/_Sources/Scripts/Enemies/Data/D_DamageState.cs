using UnityEngine;

namespace _Sources.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newDamageStateData", menuName = "Data/State Data/Damage State Data")]
    public class D_DamageState : ScriptableObject
    {
        public float DamageTime = 1f;

        public Tag HitParticles;
        public Material HitMaterial;
    }
}
