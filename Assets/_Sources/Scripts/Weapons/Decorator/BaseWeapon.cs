using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestDecorator
{
    public class BaseWeapon : IWeapon
    {
        private readonly WeaponConfig _config;
        public BaseWeapon(WeaponConfig config)
        {
            _config = config;
        }

        public float Rate
        {
            get { return _config.Rate; }
        }

        public float Range
        {
            get { return _config.Range; }
        }

        public float Strength
        {
            get { return _config.Strength; }
        }

        public float Cooldown
        {
            get { return _config.Cooldown; }
        }
    }
}
