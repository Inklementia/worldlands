using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons {
    public class AnimateBeforeAttackWeapon : MonoBehaviour
    {
        [SerializeField] private BaseWeapon baseWeapon;

        private void Start()
        {
           
        }
        private void Update()
        {

        }

        public void AnimateAttack()
        {
            baseWeapon.SetIsAnimating();
            baseWeapon.Anim.SetTrigger("attack");
            Debug.Log("Animation");
        }

        public void OnAnimationFinish()
        {
            baseWeapon.SetAnimated();
            baseWeapon.ResetIsAnimating();
        }
    }
}