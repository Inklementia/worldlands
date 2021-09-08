using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : BaseWeapon
    {
        private bool _attackAnimated;
        [SerializeField] protected string targetTag = "Enemy";

        private void Start()
        {
            Anim.SetFloat("attackSpeed", WeaponDataStats.AttackSpeed);
           
        }
        private void Update()
        {
            if (_attackAnimated)
            {
                CheckIfCanFire();

                if (CanFire)
                {
                    _attackAnimated = false;
                }
            }
        }
        public override void Attack()
        {
           

            if (CanFire && !ShouldBeCharged)
            {
                Anim.enabled = true;
                //Anim.SetTrigger("attack");
                
                CanFire = false;
            }
            else if (CanFire && (ShouldBeCharged && Charged))
            {
                CanFire = false;
                Charged = false;
                Anim.enabled = true;
                //Anim.SetTrigger("attack");

            }

         
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == targetTag)
            {
                // TODO: instaciate particles

                collision.gameObject.GetComponentInParent<HealthSystem>().Damage(WeaponDataStats.Damage);
                

            }
        }
        public void ResetCooldown()
        {
            _attackAnimated = true;
            Anim.enabled = false;
        }
      
    }
}