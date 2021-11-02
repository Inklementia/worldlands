using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitor
{
    void Visit(RotatableWeapon rotatatbleWeapon);
    void Visit(ChargeableWeapon chargeableWeapon);
    void Visit(MultishotWeapon multishotWeapon);
}
