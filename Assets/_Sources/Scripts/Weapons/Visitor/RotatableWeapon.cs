using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestVisitor
{
    public class RotatableWeapon : MonoBehaviour, IWeaponElement
    {
        public string DoNotUse = "Do another script";
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        void OnGUI()
        {
            GUI.color = Color.green;

            GUI.Label(
                new Rect(125, 0, 200, 20),
                "Rotateable");
        }
    }
}
