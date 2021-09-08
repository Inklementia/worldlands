using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    // general weapon
    public abstract class BaseWeapon : MonoBehaviour
    {
        public WeaponData WeaponDataStats;

        [SerializeField] protected Transform attackPosition;

        protected PlayerInputHandler inputHandler;

        public Animator Anim { get; protected set; }
        public ChargebleWeapon ChargebleWeapon { get; protected set; }
        public RotatableWeapon RotatableWeapon { get; protected set; }
        public AnimateBeforeAttackWeapon AnimateBeforeAttackWeapon { get; protected set; }



        public bool ShouldBeCharged { get; protected set; }
        public bool Charged { get; protected set; }
        public bool IsRotatable { get; protected set; }
        public float Angle { get; protected set; }
        public bool ShouldBeAnimatedBeforeAttack {get; protected set; }
        public bool IsAnimating { get; protected set; }
        public bool Animated { get; protected set; }

        protected bool CanFire = true;

        private float _coolDownTimer = 0.0f;
        //protected bool _attackAnimated;


        public bool Equiped { get; protected set; }

        private void Awake()
        {
            inputHandler = GetComponentInParent<PlayerInputHandler>();
            Anim = GetComponent<Animator>();

            // check for chargeableComponent
            if (GetComponent<ChargebleWeapon>() != null)
            {
                ChargebleWeapon = GetComponent<ChargebleWeapon>();
                ShouldBeCharged = true;
            }
            else
            {
                ShouldBeCharged = false;
            }
            // check for rotatable component
            if (GetComponent<RotatableWeapon>() != null)
            {
                RotatableWeapon = GetComponent<RotatableWeapon>();
                IsRotatable = true;
            }
            else
            {
                IsRotatable = false;
            }
            //check for animateBeforeAttack
            if (GetComponent<AnimateBeforeAttackWeapon>() != null)
            {
                AnimateBeforeAttackWeapon = GetComponent<AnimateBeforeAttackWeapon>();
                ShouldBeAnimatedBeforeAttack = true;
            }
            else
            {
                ShouldBeAnimatedBeforeAttack = false;
            }
        }

        // cooldown
        public void CheckIfCanFire()
        {
            if (CanFire == false)
            {
                _coolDownTimer += Time.deltaTime;
                if (_coolDownTimer >= WeaponDataStats.AttackCooldown)
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

        public void ResetAnimated()
        {
            Animated = false;
        }
        public void SetAnimated()
        {
            Animated = true;
        }
        public void SetIsAnimating()
        {
            IsAnimating = true;
        }
        public void ResetIsAnimating()
        {
            IsAnimating = false;
        }
        public abstract void Attack();
    }
}

