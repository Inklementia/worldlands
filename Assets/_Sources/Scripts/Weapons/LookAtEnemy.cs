using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Sources.Scripts.Weapons
{
    public class LookAtEnemy : MonoBehaviour
    {
        [SerializeField] private float lookSpeed;
        [SerializeField] private float fovRange = 20;
        [SerializeField] private LayerMask whatIsEnemy;
        //[SerializeField] private GameObject WhatToRotate;
        
        public GameObject enemy;
        public GameObject fovStartPoint;
        
        private float RotateAngle;
        private float InitialRotateAngle;
        private float initialAngle = 90;
        private Vector2 _lookAt;

        private void FixedUpdate()
        {
            if (EnemyInFIeldOfView(fovStartPoint))
            {
                Vector2 direction =  enemy.transform.position - transform.position;
                direction.Normalize();
                RotateAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                InitialRotateAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                if (RotateAngle < 0)
                {
                    RotateAngle = -RotateAngle;
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, -(RotateAngle - initialAngle));
                }
                else
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, -(RotateAngle - initialAngle));
                }

            }
            
            
        }    
        
        private bool EnemyInFIeldOfView(GameObject looker)
        {
            return Physics2D.CircleCast(looker.transform.position, fovRange, transform.right, 0.1f,
                whatIsEnemy);

        }
        protected virtual void OnDrawGizmos()
        {
         
      
                Gizmos.DrawWireSphere(fovStartPoint.transform.position, fovRange);
       

                Gizmos.color = Color.white;
         
        }
        

    }
}