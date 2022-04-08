using _Sources.Scripts.Interfaces;
using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class PlayerCombatSystem: CoreComponent, IDamageable
    {
        public void TakeDamage(AttackDetails attackDetails)
        {
            if (PlayerCore.ShieldSystem.CurrentStat >= attackDetails.DamageAmount)
            {
                Debug.Log("Should Damage Shield");
                PlayerCore.ShieldSystem.ShieldDamaged();
                PlayerCore.ShieldSystem.DecreaseStat(attackDetails.DamageAmount);

            }
            else
            {
                PlayerCore.ShieldSystem.ShieldDamaged();
                Debug.Log("Should Damage Health");
                Core.HealthSystem.DecreaseStat(attackDetails.DamageAmount);
            }
           

            Core.HealthSystem.IsDead = Core.HealthSystem.GetCurrentStat() <= 0 ? true : false;

        }
    }
}