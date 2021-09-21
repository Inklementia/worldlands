using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State Data")]
public class D_MeleeAttackState : ScriptableObject 
{
    public float AttackDamage = 10f;
    public float AttackRadius = .8f;

    public LayerMask WhatIsPlayer;
}
