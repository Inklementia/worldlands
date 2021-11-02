using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private BaseWeaponDataSO baseWeaponData;
    [SerializeField] private Transform attackPosition;

    //[Header("Additional weapon features")]
    public Transform AttackPosition { get => attackPosition; private set => attackPosition = value; }
    public RotatableWeapon RotatableWeapon { get; private set; }
    public ChargeableWeapon ChargeableWeapon { get; private set; }
    public MultishotWeapon MultishotWeapon { get; private set; }

    public List<IWeaponFeature> _weaponFeatures = new List<IWeaponFeature>();
    public bool CanFire { get; protected set; }
    public bool Charged { get; protected set; }
    public bool ShouldBeCharged { get; protected set; }
    public bool IsRotatable { get; protected set; }
    public float Angle { get; protected set; }

    private float _coolDownTimer = 0.0f;


    void Awake()
    {
        RotatableWeapon = gameObject.GetComponent<RotatableWeapon>();
        ChargeableWeapon = gameObject.GetComponent<ChargeableWeapon>();
        MultishotWeapon = gameObject.GetComponent<MultishotWeapon>();

        _weaponFeatures.Add(RotatableWeapon);
        _weaponFeatures.Add(ChargeableWeapon);
        _weaponFeatures.Add(MultishotWeapon);

        ShouldBeCharged = ChargeableWeapon != null ? true : false;
        IsRotatable = RotatableWeapon != null ? true : false;
    }
    private void Update()
    {
        CheckIfCanFire();
       
    }
    private void FixedUpdate()
    {
     
    }
    public void Accept(IVisitor visitor)
    {
        //foreach (IWeaponFeature element in _weaponFeatures)
        //{
        //    element.Accept(visitor);
        //}
        if(RotatableWeapon != null)
        {
            RotatableWeapon.Accept(visitor);
        }
        if (ChargeableWeapon != null)
        { 
            ChargeableWeapon.Accept(visitor);
        }
        if (MultishotWeapon != null)
        {
            MultishotWeapon.Accept(visitor);
        }

       
    }
    public void Equip()
    {
        gameObject.SetActive(true);

        if (RotatableWeapon != null)
        {
            Accept(RotatableWeapon.RotatableWeaponData);
        }
        if (ChargeableWeapon != null)
        {
            Accept(ChargeableWeapon.ChargeableWeaponData);
        }
        if (MultishotWeapon != null)
        {
            Accept(MultishotWeapon.MultishotWeaponData);
        }
        //Accept(visitor);

    }
    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    // cooldown
    public void CheckIfCanFire()
    {
        if (CanFire == false)
        {
            _coolDownTimer += Time.deltaTime;
            if (_coolDownTimer >= baseWeaponData.AttackCd)
            {
                CanFire = true;
                _coolDownTimer = 0.0f;
            }
        }
    }
    public void SetCharged()
    {
        Charged = true;
    }
    public void ResetCharged()
    {
        Charged = false;
    }
    public virtual void Attack()
    {
        if (CanFire && !ShouldBeCharged)
        {
            Debug.Log("ATTACK Not Chargeable");
            CanFire = false;
        }
        else if (CanFire && (ShouldBeCharged && Charged))
        {
            CanFire = false;
            Charged = false;
            Debug.Log("ATTACK Chargeable");
        }
    }
}
