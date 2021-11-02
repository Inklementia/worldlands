using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponFeature
{
    void Accept(IVisitor visitor);
}
