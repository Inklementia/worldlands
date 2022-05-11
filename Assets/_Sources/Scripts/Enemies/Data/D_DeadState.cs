using UnityEngine;

namespace _Sources.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State Data")]
    public class D_DeadState : ScriptableObject
    {
        public Tag DeathParticlesTag;
 
    }
}
