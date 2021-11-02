using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TestDecorator
{
    public class WeaponDecorator : IWeapon
    {
        private readonly IWeapon _decoratedWeapon;
        private readonly WeaponAttachment _attachment;

        public WeaponDecorator(IWeapon decoratedWeapon, WeaponAttachment attachment)
        {
            _decoratedWeapon = decoratedWeapon;
            _attachment = attachment;
        }

        public float Rate
        {
            get { return _decoratedWeapon.Rate + _attachment.Rate; }
        }

        public float Range
        {
            get { return _decoratedWeapon.Range + _attachment.Range; }
        }

        public float Strength
        {
            get { return _decoratedWeapon.Strength + _attachment.Strength; }
        }

        public float Cooldown
        {
            get { return _decoratedWeapon.Cooldown + _attachment.Cooldown; }
        }


    }
}