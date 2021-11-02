using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : Core
{
    public CollisionSenses CollisionSenses { get; private set; }
    public PlayerDetectionSenses PlayerDetectionSenses { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        PlayerDetectionSenses = GetComponentInChildren<PlayerDetectionSenses>();

        if (!CollisionSenses || !PlayerDetectionSenses)
        {
            Debug.LogError("Missing Senses Component");
        }

    }
}
