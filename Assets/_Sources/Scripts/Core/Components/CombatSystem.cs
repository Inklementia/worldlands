using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class CombatSystem : CoreComponent, IKnockable, IDamageable
    {
        [SerializeField] private float knockBackSpeed = 2;
        [SerializeField] private Vector2 knockBackAngle = new Vector2 (1,0);
        [SerializeField] private float knockbackDuration = 1f;
    
       
        
        private float _knockbackStartTime;
        private int _lastDamageDirection;

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
        
        // DAMAGE 
        public virtual void TakeDamage(AttackDetails attackDetails)
        { 
       
            
            if(attackDetails.Position.x > transform.position.x)
            {
                _lastDamageDirection = -1;
            }
            else
            {
                _lastDamageDirection = 1;
            }
            
            Core.HealthSystem.DecreaseStat(attackDetails.DamageAmount);
            
            Core.HealthSystem.IsDead = Core.HealthSystem.GetCurrentStat() <= 0 ? true : false;

        }
    }
}