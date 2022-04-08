using _Sources.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class StatsComponent : MonoBehaviour, IStats
    {
        public float CurrentStat { get; private set; }
        public float MaxStat { get; private set; }

        protected Core Core;

        protected virtual void Awake()
        {
            Core = transform.parent.GetComponent<Core>();

            if (Core == null)
            {
                Debug.LogError("No Core on the parent");
            }

        }

        public float GetCurrentStat()
        {
            return CurrentStat;
        }
        public void SetMaxStat(float maxHealth)
        {
            MaxStat = maxHealth;
            CurrentStat = maxHealth;
        }

        public virtual  void DecreaseStat(float damageAmount)
        {
            CurrentStat -= damageAmount;
            if (CurrentStat < 0)
            {
                CurrentStat = 0;
                //Destroy(gameObject);
            }
            
            
        }

        public virtual void IncreaseStat(float healAmount)
        {
            CurrentStat += healAmount;
            if (CurrentStat > MaxStat)
            {
                CurrentStat = MaxStat;
            }
           
           
        }

    }

}