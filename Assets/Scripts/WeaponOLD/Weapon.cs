using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponOld : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;


    protected PlayerInputHandler inputHandler { get; private set; }

    private void Awake()
    {
        inputHandler = FindObjectOfType<PlayerInputHandler>();
    }
    public abstract void Attack();

   
}
