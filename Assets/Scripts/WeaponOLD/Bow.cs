using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : WeaponOld
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Slider bowPowerSlider;
    [Range(0, 10)]
    [SerializeField] private float bowPower;
    [Range(0, 3)]
    [SerializeField] private float maxBowCharge;

    private float _bowCharge;

    private bool _canFire = true;
    private bool _charged;


    //remove

    [SerializeField] private float travelDistance = 5f;
    [SerializeField] private float lifeDuration = 6f;
    [SerializeField] private float dragMultiplier = .5f;

    public override void Attack()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        bowPowerSlider.value = 0f;
        bowPowerSlider.maxValue = maxBowCharge;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputHandler._isAttackButtonPressed && _canFire)
        {
            ChargeBow();
        }
        else if (!inputHandler._isAttackButtonPressed && _canFire && _charged)
        {
            FireBow();
        }
        else
        {
            if(_bowCharge > 0f)
            {
                _bowCharge -= 1f * Time.deltaTime;
            }
            else
            {
                _bowCharge = 0f;
                _canFire = true;
            }
        }
    }

    private void ChargeBow()
    {
        // show arrow and do bow animation
        Debug.Log("CHARGING");

        _bowCharge += Time.deltaTime;
        bowPowerSlider.value = _bowCharge;

        if(_bowCharge > maxBowCharge)
        {
            bowPowerSlider.value = maxBowCharge;
        }
        _charged = true;
    }

    private void FireBow()
    {
        if (_bowCharge > maxBowCharge) 
        {
            _bowCharge = maxBowCharge;
        }

        float arrowSpeed = _bowCharge + bowPower;

        GameObject bullet = Instantiate(arrow, firePoint.position, firePoint.rotation);
        var direction = transform.right;
        ProjectileOld projectile = bullet.GetComponent<ProjectileOld>();
        projectile.FireProjectile(arrowSpeed, direction, travelDistance, lifeDuration, dragMultiplier);

        _canFire = false;
        _charged = false;
    } 
}
