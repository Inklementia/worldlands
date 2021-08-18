using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class ChargebleWeapon : MonoBehaviour
    {
        [SerializeField] private BaseWeapon baseWeapon;
       
        [SerializeField] private Slider chargeSlider;

        private PlayerInputHandler inputHandler;
        private WeaponData _weaponData;

        private float _maxChargeValue;
        private float _chargeValue;
        public float ChargedProjectileSpeed { get; private set; }

        private void Start()
        {
            inputHandler = GetComponentInParent<PlayerInputHandler>();
            _weaponData = baseWeapon.BaseWeaponData;

            _maxChargeValue = _weaponData.MaxCharge;
            chargeSlider.value = 0f;
            chargeSlider.maxValue = _weaponData.MaxCharge;

        }

        private void Update()
        {
            baseWeapon.Anim.SetBool("charge", inputHandler._isAttackButtonPressed);

            if (!inputHandler._isAttackButtonPressed)
            {
                if (_chargeValue > 0f)
                {
                    _chargeValue -= 2f * Time.deltaTime;
                    chargeSlider.value = _chargeValue;
                }
                else
                {
                    _chargeValue = 0f;
                    //baseWeapon.ResetCharge();

                }
            }
        }

        public void Charge()
        {
            // show arrow and do bow animation
           // Debug.Log("CHARGING");

            _chargeValue += Time.deltaTime;
            chargeSlider.value = _chargeValue;

            baseWeapon.SetCharge();
           

            // not to exceed
            if (_chargeValue > _maxChargeValue)
            {
                _chargeValue = _maxChargeValue;
                chargeSlider.value = _maxChargeValue;
            }
            ApplyChargedPower();
        }
        
        private void ApplyChargedPower()
        {
            ChargedProjectileSpeed = _chargeValue + _weaponData.ProjectileSpeed;
        }
        //public override void Attack()
        //{
        //    if (_charge > _maxCharge)
        //    {
        //        _charge = _maxCharge;
        //    }

           
        //    float arrowSpeed = _charge + power;

        //    _wasCharging = false;
        //    _canCharge = false;

        //    _canFire = true;


        //    Debug.Log("Attack");
        //}
    }
}
