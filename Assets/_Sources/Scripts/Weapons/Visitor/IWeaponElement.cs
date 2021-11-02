using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestVisitor
{
    public interface IWeaponElement
    {
        void Accept(IVisitor visitor);
    }
}

   