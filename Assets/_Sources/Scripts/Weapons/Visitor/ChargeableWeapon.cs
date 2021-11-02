using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestVisitor
{
    public class ChargeableWeapon : MonoBehaviour, IWeaponElement
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        void OnGUI()
        {
            GUI.color = Color.green;

            GUI.Label(
                new Rect(125, 20, 200, 20),
                "Chargeablee");
        }
    }
}