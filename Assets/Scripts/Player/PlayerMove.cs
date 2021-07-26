using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerAction
{
    [SerializeField] private float speed;

    private void Update()
    {
        anim.SetBool("run", inputHandler._isPlayerMoving);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputHandler._movementPosX, inputHandler._movementPosY) * speed;

        if (inputHandler._isJoystickPressed)
        {
            HandleFlip();
        }
    }

    private void HandleFlip()
    {
        if (inputHandler._movementPosX > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (inputHandler._movementPosX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
