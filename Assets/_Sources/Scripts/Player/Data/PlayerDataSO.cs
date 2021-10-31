using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerDataSO : ScriptableObject
{
    public float MaxHealth = 100;

    [Header("Move State")]
    public float MovementVelocity = 4f;

 

}
