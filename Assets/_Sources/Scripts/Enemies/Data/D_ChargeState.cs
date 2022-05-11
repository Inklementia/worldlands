using UnityEngine;

namespace _Sources.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State Data")]
    public class D_ChargeState : ScriptableObject
    {

        public float ChargeSpeed = 6f;

        public float ChargeTime = 2f;
    }
}
