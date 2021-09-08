using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChaseArea : MonoBehaviour
{
    public bool _isInTriggerChaseArea { get; private set; }
    [SerializeField] private Tag tagToDetect;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.HasTag(tagToDetect))
        {
            _isInTriggerChaseArea = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.HasTag(tagToDetect))
        {
            _isInTriggerChaseArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInTriggerChaseArea = false;
    }
}
