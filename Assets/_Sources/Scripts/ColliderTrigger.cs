using System;
using UnityEngine;

namespace _Sources.Scripts.Battle
{
    public class ColliderTrigger : MonoBehaviour
    {
        [SerializeField] private Tag triggerTag;

        public event EventHandler OnEnterTrigger;
       
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.HasTag(triggerTag))
            {
                OnEnterTrigger?.Invoke(this, EventArgs.Empty);
            }

        
        }
    }
}