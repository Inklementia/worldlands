
using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Helpers;
using UnityEngine;

namespace _Sources.Scripts.Core.Components
{
    public class EnergySystem : StatsComponent
    {

        public override void IncreaseStat(float amount)
        {
            base.IncreaseStat(amount);

            GameActions.Instance.ChangeEnergyValue(CurrentStat, false);
        }
        
        public override void DecreaseStat(float amount)
        {
            base.DecreaseStat(amount);

            GameActions.Instance.ChangeEnergyValue(CurrentStat, false);
        }

    }
}

