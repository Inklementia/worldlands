using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestVisitor
{
    public class MultishotWeapon : MonoBehaviour, IWeaponElement
    {
        public string DonotUse;
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        void OnGUI()
        {
            GUI.color = Color.green;

            GUI.Label(
                new Rect(125, 40, 200, 20),
                "Multishot");
        }
    }
}
