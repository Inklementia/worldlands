using UnityEngine;

namespace _Sources.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State Data")]
    public class D_MoveState : ScriptableObject
    {
        public float MovementSpeed;
    }
}
