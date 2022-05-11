using System;
using System.Collections;
using _Sources.Scripts.Data;
using _Sources.Scripts.Helpers;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider energyBar;
        [SerializeField] private Slider shieldBar;
        [SerializeField] private float rechargeSpeed = 5f;
        private float _maxHealthValue;
        private float _currentHealthValue;
        
        private float _maxEnergyValue;
        private float _currentEnergyValue;
        
        private float _maxShieldValue;
        private float _currentShieldValue;

        [SerializeField] private Material shineMaterial;
        public void SetUI( PlayerEntity player)
        {
            _maxHealthValue = player.Core.HealthSystem.MaxStat;
            _currentHealthValue = player.Core.HealthSystem.CurrentStat;
                
            _maxEnergyValue = player.Core.EnergySystem.MaxStat;
            _currentEnergyValue = player.Core.HealthSystem.CurrentStat;
            
            _maxShieldValue = player.Core.ShieldSystem.MaxStat;
            _currentShieldValue = player.Core.ShieldSystem.MaxStat;


            healthBar.maxValue = _maxHealthValue;
            healthBar.value = _currentHealthValue;

            energyBar.maxValue = _maxEnergyValue;
            energyBar.value = _currentEnergyValue;
            
            //max shield always
            shieldBar.maxValue = _maxShieldValue;
            shieldBar.value = _currentShieldValue;
        }

  

        private void OnEnable()
        {
            GameActions.Instance.OnShieldChange += ChangeShieldValue;
            GameActions.Instance.OnHealthChange += ChangeHealthValue;
            GameActions.Instance.OnEnergyChange += ChangeEnergyValue;
        }

        private void OnDisable()
        {
            GameActions.Instance.OnShieldChange -= ChangeShieldValue;
            GameActions.Instance.OnHealthChange -= ChangeHealthValue;
            GameActions.Instance.OnEnergyChange -= ChangeEnergyValue;
        }

        private void ChangeShieldValue(float amount, bool reset)
        {
            if (reset)
            {
                shieldBar.DOValue(amount, rechargeSpeed);
            }
            else
            {
                shieldBar.DOValue(amount, 1f);
            }

            //ShineBar();

        }
        private void ChangeHealthValue(float amount, bool reset)
        {
            if (reset)
            {
                healthBar.DOValue(amount, rechargeSpeed);
            }
            else
            {
                healthBar.DOValue(amount, 1f);
            }

            //ShineBar();
        }
        private void ChangeEnergyValue(float amount, bool reset)
        {
            if (reset)
            {
                energyBar.DOValue(amount, rechargeSpeed);
            }
            else
            {
                energyBar.DOValue(amount, 1f);
            }

            //ShineBar();
        }

        /*
        private void ShineBar()
        {
            float random = Random.Range(0f, 100f);
            if (random % 2 == 0)
            {
                shineMaterial.DOFloat(1, "_ShineLocation", 1f);
            }
        }*/
    }
}