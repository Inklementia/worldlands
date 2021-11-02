using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private BaseWeaponDataSO BaseWeaponData;
    [SerializeField] private Transform AttackPosition;

    //[Header("Additional weapon features")]
    private RotatableWeapon rotatable;
    private ChargeableWeapon chargeable;
    private MultishotWeapon multishot;

    public List<IWeaponFeature> _weaponFeatures = new List<IWeaponFeature>();
    public bool CanFire { get; protected set; }
    public bool Charged { get; protected set; }

    private float _coolDownTimer = 0.0f;


    void Start()
    {
        rotatable = gameObject.GetComponent<RotatableWeapon>();
        chargeable = gameObject.GetComponent<ChargeableWeapon>();
        multishot =  gameObject.GetComponent<MultishotWeapon>();
        _weaponFeatures.Add(rotatable);
        _weaponFeatures.Add(chargeable);
        _weaponFeatures.Add(multishot);
    }
    private void Update()
    {
        CheckIfCanFire();
    }
    public void Accept(IVisitor visitor)
    {
        //foreach (IWeaponFeature element in _weaponFeatures)
        //{
        //    element.Accept(visitor);
        //}
        rotatable.Accept(visitor);
        chargeable.Accept(visitor);
        //multishot.Accept(visitor);
    }
    public void Equip()
    {
        gameObject.SetActive(true);

        //foreach (var item in _weaponFeatures)
        //{
        //    Accept(item.GetComponent<IWeaponFeature>());
        //}
       
        Accept(chargeable.chargeable);
        Accept(rotatable.rotatable);
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
            if (_coolDownTimer >= BaseWeaponData.AttackCd)
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
    public void ResetCharge()
    {
        Charged = false;
    }
    public virtual void Attack()
    {
        if (CanFire && chargeable == null)
        {
            Debug.Log("ATTACK");
            CanFire = false;
        }
        else if (CanFire && (chargeable != null && Charged))
        {
            CanFire = false;
            Charged = false;
            Debug.Log("ATTACK");


        }
    }


}
