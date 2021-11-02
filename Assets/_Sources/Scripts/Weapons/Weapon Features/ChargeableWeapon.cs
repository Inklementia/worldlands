using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeableWeapon : MonoBehaviour, IWeaponFeature
{
    [SerializeField] private WeaponTypeDataSO chargeableWeaponData;

    [SerializeField] private Slider chargeSlider;
    [SerializeField] private Animator anim;
    [SerializeField] private ProjectileDataSO projectileData;

    private bool _canFireWithoutCharge;

    private float _maxChargeValue;
    private float _chargeValue;

    private Player _player;
    private Weapon _weapon;
    public WeaponTypeDataSO ChargeableWeaponData { get => chargeableWeaponData; private set => chargeableWeaponData = value; }

    public float ChargedProjectileSpeed { get; private set; }


    private void Awake()
    {
        _canFireWithoutCharge = ChargeableWeaponData.CanFireWithoutCharge;
        _maxChargeValue = ChargeableWeaponData.MaxCharge;

        chargeSlider.value = 0f;
        chargeSlider.maxValue = ChargeableWeaponData.MaxCharge;

        _weapon = GetComponent<Weapon>();
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
        }
    }

    // called from player weapon handler (because we need to know how played clicks attack button)
    public void Charge()
    {
        // show arrow and do bow animation
         Debug.Log("CHARGING");

        _chargeValue += Time.deltaTime;
        chargeSlider.value = _chargeValue;


        if (!_canFireWithoutCharge)
        {
            if (_chargeValue > .8f)
            {
                _weapon.SetCharged();
            }
        }
        else
        {
            _weapon.SetCharged();
        }
   

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
        ChargedProjectileSpeed = _chargeValue + projectileData.ProjectileSpeed;
    }

    public void Accept(IVisitor visitor)
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //_weapon = GetComponent<Weapon>();
        visitor.Visit(this);
    }
}
