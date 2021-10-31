using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }

    public CollisionSenses CollisionSenses { get; private set; }

    protected virtual void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();

        if (!Movement)
        {
            Debug.LogError("Missing Movement Component");
        }
    }

    //public void LogicUpdate()
    //{
    //    Movement.LogicUpdate();
    //}
}
