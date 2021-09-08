using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttackArea : MonoBehaviour
{
    public bool _isInTriggerAttackArea { get; private set; }
    [SerializeField] private Tag tagToDetect;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.HasTag(tagToDetect))
        {
            _isInTriggerAttackArea = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.HasTag(tagToDetect))
        {
            _isInTriggerAttackArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInTriggerAttackArea = false;
    }
}


