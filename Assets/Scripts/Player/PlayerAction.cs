using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction : MonoBehaviour
{
    public PlayerInputHandler inputHandler { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    private void Start()
    {

        anim = GetComponent<Animator>();
        inputHandler = GetComponentInParent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
    }
}
