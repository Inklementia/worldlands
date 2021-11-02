using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }

    protected virtual void Awake()
    {
        Movement = GetComponentInChildren<Movement>();

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
