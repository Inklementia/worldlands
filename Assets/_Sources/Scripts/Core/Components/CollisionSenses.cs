using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class CollisionSenses : CoreComponent
    {
        //public Transform WallCheck
        //{
        //    get => wallCheck; private set => wallCheck = value;
        //}

        [SerializeField] private Transform wallCheck;

        [SerializeField] private float wallCheckDistance = 1f;

        [SerializeField] private LayerMask whatIsWall;

   
        public bool Wall
        {
            get => Physics2D.CircleCast(wallCheck.position, wallCheckDistance, transform.right, 0.1f, whatIsWall);
        }

        protected virtual void OnDrawGizmos()
        {
            if(Core != null)
            {
                Gizmos.DrawWireSphere(wallCheck.position, wallCheckDistance);
            }
        }
    }
}
