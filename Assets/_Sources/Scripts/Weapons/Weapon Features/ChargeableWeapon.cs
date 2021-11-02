using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeableWeapon : MonoBehaviour, IWeaponFeature
{
    [SerializeField] private Slider chargeSlider;
    [SerializeField] private Animator anim;

    public AdditionalWeaponFeatureDataSO chargeable;

    private float _maxChargeValue;
    private float _chargeValue;
    private Player _player;
    private Weapon _weapon;
    public float ChargedProjectileSpeed { get; private set; }

    public void Accept(IVisitor visitor)
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _weapon = GetComponent<Weapon>();
        visitor.Visit(this);
    }
    private void Awake()
    {
        _maxChargeValue = chargeable.MaxCharge;
        chargeSlider.value = 0f;
        chargeSlider.maxValue = chargeable.MaxCharge;
    }
    private void Update()
    {
        if(_player != null)
        {
            anim.SetBool("charge", _player.InputHandler.IsAttackButtonPressed);

            if (!_player.InputHandler.IsAttackButtonPressed)
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
            else
            {
                Charge();
            }
        }
    }

    public void Charge()
    {
        // show arrow and do bow animation
        // Debug.Log("CHARGING");

        _chargeValue += Time.deltaTime;
        chargeSlider.value = _chargeValue;

        _weapon.SetCharged();


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
        ChargedProjectileSpeed = _chargeValue + chargeable.ProjectileSpeed;
    }
}
