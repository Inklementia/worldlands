using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotWeapon : MonoBehaviour, IWeaponFeature
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
