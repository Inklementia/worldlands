using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    // general weapon
    public abstract class BaseWeapon : MonoBehaviour
    {
        public WeaponData BaseWeaponData;

        [SerializeField] protected Transform attackPosition;

        protected PlayerInputHandler inputHandler;

        public Animator Anim { get; protected set; }
        public ChargebleWeapon ChargebleWeapon { get; protected set; }
        public bool ShouldBeCharged { get; protected set; }
        public bool Charged { get; protected set; }


        public bool CanFire { get; protected set; }

        private float _coolDownTimer = 0.0f;

        private void Awake()
        {
            inputHandler = GetComponentInParent<PlayerInputHandler>();
            Anim = GetComponent<Animator>();

            if (GetComponent<ChargebleWeapon>() != null)
            {
                ChargebleWeapon = GetComponent<ChargebleWeapon>();
                ShouldBeCharged = true;
            }
            else
            {
                ShouldBeCharged = false;
            }
        }


        // cooldown
        public void CheckIfCanFire()
        {
            if (CanFire == false)
            {
                _coolDownTimer += Time.deltaTime;
                if (_coolDownTimer >= BaseWeaponData.AttackCooldown)
                {
                    CanFire = true;
                    _coolDownTimer = 0.0f;
                }
            }
        }

        public void ResetCharge()
        {
            Charged = false;
        }
        public void SetCharge()
        {
            Charged = true;
        }
        public abstract void Attack();
    }
}

