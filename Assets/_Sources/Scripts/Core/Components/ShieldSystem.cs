using System;
using System.Collections;
using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class ShieldSystem : StatsComponent
    {
        [SerializeField] private float rechargeCd = 1f;
        [SerializeField] private float requiredTimeWithoutDamage = 2f;
        private float _shieldRechargeTimer;
        private float _noDamageTimer = 0.0f;
        private bool _shouldCheck;
  
        public void LogicUpdate()
        {
            if (_shouldCheck)
            {
                _noDamageTimer += Time.deltaTime;
            }

            CheckRecharge();
        }
        
        public void ShieldDamaged()
        {
            _noDamageTimer = 0.0f;
            _shouldCheck = true;
        }
        private void CheckRecharge()
        {
            if (_noDamageTimer >= requiredTimeWithoutDamage )
            {
                _shouldCheck = false;
                GameActions.Instance.ChangeShieldValue(MaxStat, true);
                StartCoroutine(ResetShield());
                //_noDamageTimer = 0.0f;
            }
           
        }

        private IEnumerator ResetShield()
        {
            yield return new WaitForSeconds(2f);
            IncreaseStat(MaxStat);
        }
        public override void IncreaseStat(float amount)
        {
            base.IncreaseStat(amount);

            GameActions.Instance.ChangeShieldValue(CurrentStat, false);
        }
        
        public override void DecreaseStat(float amount)
        {
            base.DecreaseStat(amount);

            GameActions.Instance.ChangeShieldValue(CurrentStat, false);
        }
    }
}
