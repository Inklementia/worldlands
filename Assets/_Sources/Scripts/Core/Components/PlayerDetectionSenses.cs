using System;
using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class PlayerDetectionSenses : CoreComponent
    {
        //public Transform PlayerCheck
        //{
        //    get => playerCheck; private set => playerCheck = value;
        //}

        [SerializeField] private Transform playerCheck;

        [SerializeField] private float maxAgroDistance = 5f; // for stopping to  chase / detect
        [SerializeField] private float minAgroDistance = 4f; // for chase / detect

        [SerializeField] private float closeRangeActionDistance = 1f; // for attack

        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Tag playerTag;
        public GameObject Player { get; private set; }


       // public bool InCloseRangeAction = false;
        
        protected override void Awake()
        {
            base.Awake();
            //Player = gameObject.FindWithTag(playerTag);
           
            
        }

 

        private void Start()
        {
            Player = gameObject.FindWithTag(playerTag);
        }

        public bool InMinAgroRange
        {
            get => Physics2D.CircleCast(playerCheck.position, minAgroDistance, transform.right, 0.1f, whatIsPlayer);
        }

        public bool InMaxAgroRange
        {
            get => Physics2D.CircleCast(playerCheck.position, maxAgroDistance, transform.right, 0.1f, whatIsPlayer);
        }

        //in melee attack Range
       
        public bool InCloseRangeAction
        {
            get => Physics2D.OverlapCircle(playerCheck.position, closeRangeActionDistance, whatIsPlayer);
        }
 /*
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.HasTag(playerTag))
            {
                InCloseRangeAction = true;
            }
        }
        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.HasTag(playerTag))
            {
                InCloseRangeAction = true;
            }
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            InCloseRangeAction = false;
            
        }*/
        protected virtual void OnDrawGizmos()
        {
            if (Core != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(playerCheck.position, minAgroDistance);
                Gizmos.DrawWireSphere(playerCheck.position, maxAgroDistance);

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(playerCheck.position, closeRangeActionDistance);

                Gizmos.color = Color.white;
            }
        }
    }
}
