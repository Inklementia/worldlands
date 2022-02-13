using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace _Sources.Scripts.Weapons.Weapon_Features
{
    public class ChargeableWeapon : MonoBehaviour, IWeaponFeature
    {
        [SerializeField] private WeaponTypeDataSO chargeableWeaponData;
        [SerializeField] private Tag chargeSliderTag;
        private Slider _chargeSlider;
        [SerializeField] private Animator anim;

        private bool _canFireWithoutCharge;

        private float _maxChargeValue;
        private float _chargeValue;

        private PlayerEntity _playerEntity;
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
            if(_playerEntity != null)
            {
                anim.SetBool("charge", _playerEntity.InputHandler.IsAttackButtonPressed);

                if (!_playerEntity.InputHandler.IsAttackButtonPressed)
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
            ChargedProjectileSpeed = _chargeValue + _weapon.BaseWeaponData.ProjectileSpeed;
        }

        public void Accept(IVisitor visitor)
        {
            _playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerFiniteStateMachine.PlayerEntity>();

            _chargeSlider = gameObject.FindWithTag(chargeSliderTag).GetComponent<Slider>();
            _chargeSlider.value = 0f;
            _chargeSlider.maxValue = ChargeableWeaponData.MaxCharge;
            //_weapon = GetComponent<Weapon>();
            visitor.Visit(this);
        }

        public void UnsetPlayer()
        {
            _playerEntity = null;
        }
    }
}
