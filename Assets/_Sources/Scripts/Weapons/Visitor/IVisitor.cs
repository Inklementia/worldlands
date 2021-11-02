using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestVisitor
{
    public interface IVisitor
    {
        void Visit(RotatableWeapon bikeShield);
        void Visit(ChargeableWeapon bikeEngine);
        void Visit(MultishotWeapon bikeWeapon);
    }
}