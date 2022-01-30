using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeableWeapon : MonoBehaviour, IWeaponFeature
{
    [SerializeField] private WeaponTypeDataSO chargeableWeaponData;
    [SerializeField] private Tag chargeSliderTag;
    private Slider _chargeSlider;
    [SerializeField] private Animator anim;

    private bool _canFireWithoutCharge;

    private float _maxChargeValue;
    private float _chargeValue;

    private Player _player;
    private ShootingWeapon _weapon;
    public WeaponTypeDataSO ChargeableWeaponData { get => chargeableWeaponData; private set => chargeableWeaponData = value; }

    public float ChargedProjectileSpeed { get; private set; }


    private void Awake()
    {
        _canFireWithoutCharge = ChargeableWeaponData.CanFireWithoutCharge;
        _maxChargeValue = ChargeableWeaponData.MaxCharge;

       

        _weapon = GetComponent<ShootingWeapon>();
    }
    private void Update()
    {
        if(_player != null)
        {
            anim.SetBool("charge", _player.InputHandler.IsAttackButtonPressedDown);

            if (!_player.InputHandler.IsAttackButtonPressedUp)
            {
                if (_chargeValue > 0f)
                {
                    _chargeValue -= 2f * Time.deltaTime;
                    _chargeSlider.value = _chargeValue;
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
        _chargeSlider.value = _chargeValue;


        if (!_canFireWithoutCharge)
        {
            if (_chargeValue > chargeableWeaponData.MinCharge)
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
            _chargeSlider.value = _maxChargeValue;
        }
        ApplyChargedPower();
    }

    private void ApplyChargedPower()
    {
        ChargedProjectileSpeed = _chargeValue + _weapon.baseWeaponData.ProjectileSpeed;
    }

    public void Accept(IVisitor visitor)
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _chargeSlider = gameObject.FindWithTag(chargeSliderTag).GetComponent<Slider>();
        _chargeSlider.value = 0f;
        _chargeSlider.maxValue = ChargeableWeaponData.MaxCharge;
        //_weapon = GetComponent<Weapon>();
        visitor.Visit(this);
    }

    public void UnsetPlayer()
    {
        _player = null;
    }
}
