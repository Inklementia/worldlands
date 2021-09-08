using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageble : MonoBehaviour
{
    public delegate void Damaged(int damageAmount);
    public static event Damaged OnDamage;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DamageZone")
        {
            if (OnDamage != null)
                OnDamage(10);
        }
        if (col.gameObject.tag == "BigDamage")
        {
            if (OnDamage != null)
                OnDamage(50);
        }
    }
}
