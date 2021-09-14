using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPatrolStateData", menuName = "Data/State Data/Patrol State Data")]
public class D_PatrolState : ScriptableObject
{
    public float MovementSpeed = 3f;
    public float PatrolDistance = 6f;
}
