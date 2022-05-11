using _Sources.Scripts.Helpers;
using _Sources.Scripts.Interfaces;
using _Sources.Scripts.Structs;
using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class PlayerCombatSystem: CoreComponent, IDamageable, IKnockable
    {
        [SerializeField] private float knockBackSpeed = 2;
        [SerializeField] private Vector2 knockBackAngle = new Vector2 (1,0);
        [SerializeField] private float knockbackDuration = 1f;

        private float _knockbackStartTime;
        private int _lastDamageDirection;
        
        public void TakeDamage(AttackDetails attackDetails)
        {
            
            if(attackDetails.Position.x > transform.position.x)
            {
                _lastDamageDirection = -1;
            }
            else
            {
                _lastDamageDirection = 1;
            }
            
            if (PlayerCore.ShieldSystem.CurrentStat >= attackDetails.DamageAmount)
            {
                Debug.Log("Should Damage Shield");
                PlayerCore.ShieldSystem.ShieldDamaged();
                PlayerCore.ShieldSystem.DecreaseStat(attackDetails.DamageAmount);

                //GameActions.Instance.ChangeShieldValue(PlayerCore.ShieldSystem.CurrentStat, false);

            }
            else
            {
                GameActions.Instance.ChangeHealthValue(PlayerCore.HealthSystem.CurrentStat, false);
                PlayerCore.ShieldSystem.ShieldDamaged();
                Debug.Log("Should Damage Health");
                Core.HealthSystem.DecreaseStat(attackDetails.DamageAmount);
            }
           

            Core.HealthSystem.IsDead = Core.HealthSystem.GetCurrentStat() <= 0 ? true : false;

        }

        public void LogicUpdate()
        {
            CheckKnockback();
        }
        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            _knockbackStartTime = Time.time;
   
            Core.Movement.SetVelocity(knockBackAngle, knockBackSpeed, _lastDamageDirection);
        }
        public void CheckKnockback()
        {
            if (Time.time >= _knockbackStartTime + knockbackDuration)
            {
                Core.Movement.SetVelocityZero();
            }
        }

    }
}