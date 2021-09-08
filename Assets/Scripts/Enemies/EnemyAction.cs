using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : MonoBehaviour
{
    [SerializeField] protected GameObject enemyToFlip;
    [SerializeField] protected TriggerChaseArea triggerChaseArea;
    [SerializeField] protected TriggerAttackArea triggerAttackArea;

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject _player { get; private set; }

    protected bool _canMove;

    protected virtual void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

}
