using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player Player { get; private set; }
    public Weapon()
    {
    }

    public void Equip()
    {
        gameObject.SetActive(true);
    }
    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

   
}
