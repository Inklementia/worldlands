using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : Weapon
{
    [SerializeField] protected Tag PlayerTag;
    protected Player Player;

    private void OnEnable()
    {
       
    }
    private void Start()
    {
        Player = gameObject.FindWithTag(PlayerTag).GetComponent<Player>();
    }


}
