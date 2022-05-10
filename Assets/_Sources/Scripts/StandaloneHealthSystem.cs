using _Sources.Scripts.Interfaces;
using UnityEngine;

namespace _Sources.Scripts
{
    public class StandaloneHealthSystem : MonoBehaviour, IStats
    {
        public float CurrentStat { get; private set; }
        public float MaxStat { get; private set; }



        public float GetCurrentStat()
        {
            return CurrentStat;
        }
        public void SetMaxStat(float maxHealth)
        {
            MaxStat = maxHealth;
            CurrentStat = maxHealth;
        }

        public void DecreaseStat(float damageAmount)
        {
            CurrentStat -= damageAmount;
            if (CurrentStat < 0)
            {
                CurrentStat = 0;
                //Destroy(gameObject);
            }
        }

        public void IncreaseStat(float healAmount)
        {
            CurrentStat += healAmount;
            if (CurrentStat > MaxStat)
            {
                CurrentStat = MaxStat;
            }
        }

    }
}