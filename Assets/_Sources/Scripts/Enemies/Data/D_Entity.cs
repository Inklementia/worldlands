using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float KnockBackSpeed = 2;
    public Vector2 KnockBackAngle = new Vector2 (1,0);
    public float KnockbackDuration = 1f;
}
