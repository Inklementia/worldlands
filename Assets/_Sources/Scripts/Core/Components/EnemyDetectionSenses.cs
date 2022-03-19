using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class EnemyDetectionSenses : CoreComponent
    {

        [SerializeField] private Transform enemyCheck;

        [SerializeField] private float FOVRange = 6f; // for stopping to  chase / detect

        [SerializeField] private LayerMask whatIsEnemy;
        [SerializeField] private Tag enemyTag;
        
        
        public List<GameObject> EnemiesList { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            EnemiesList = new List<GameObject>();
        }

        public void LogicUpdate()
        {
      
        }
        
        public bool EnemyInFieldOfView()
        {

            return Physics2D.CircleCast(enemyCheck.position, FOVRange, transform.right, 0.08f,
                whatIsEnemy);

        }
        public void DetectEnemiesInFieldOfView()
        {
            EnemiesList.Clear();
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(enemyCheck.position, FOVRange, whatIsEnemy);
            foreach (Collider2D collider in detectedObjects)
            {
                Debug.Log("ENEMY");
                EnemiesList.Add(collider.gameObject);
                
               
            }
            /*return Physics2D.CircleCast(enemyCheck.position, FOVRange, transform.right, 0.08f,
                whatIsEnemy);*/
         
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.HasTag(enemyTag))
            {
                EnemiesList.Add(col.gameObject);
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.HasTag(enemyTag))
            {
                EnemiesList.Remove(col.gameObject);
            }
        }
    }
}
