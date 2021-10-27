using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void Equip()
    {
        gameObject.SetActive(true);
    }
    public void UnEquip()
    {
        gameObject.SetActive(false);
    }
}
