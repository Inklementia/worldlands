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

        public bool EnemyInFieldOfView()
        {
            return Physics2D.CircleCast(enemyCheck.position, FOVRange, transform.right, 0.1f,
                whatIsEnemy);

        }
  
    }
}
