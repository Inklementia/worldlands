using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core Core;

    protected virtual void Awake()
    {
        Core = transform.parent.GetComponent<Core>();

        if(Core == null)
        {
            Debug.LogError("No Core on the parent");
        }
    }
}
