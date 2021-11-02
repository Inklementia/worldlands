using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestDecorator
{
    public interface IWeapon
    {
        float Rate { get; }
        float Range { get; }
        float Strength { get; }
        float Cooldown { get; }
    }
}