
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D Rb { get; private set; }

    //public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 _workSpace;

    protected override void Awake()
    {
        base.Awake();
        Rb = GetComponentInParent<Rigidbody2D>();
    }
    //public void LogicUdpate()
    //{
    //    CurrentVelocity = Rb.velocity;
    //}

    public void SetVelocity(float velocityX, float velocityY)
    {
        _workSpace.Set(velocityX, velocityY);
        //CurrentVelocity = _workSpace;
        Rb.velocity = _workSpace;
    }
    public void SetVelocityZero()
    {
        //_workSpace = Vector2.zero;
        Rb.velocity = Vector2.zero;
        //CurrentVelocity = Vector2.zero;
    }

    //used for enemies
    public virtual void SetVelocity(Vector2 direction, float speed)
    {
        direction.Normalize();
        _workSpace = direction * speed;
        Rb.velocity = _workSpace;
    }
    public virtual void SetVelocity(Vector2 angle, float speed, int direction)
    {
        angle.Normalize();
        _workSpace.Set(angle.x * speed * direction, angle.y * speed * direction);
        Rb.velocity = _workSpace;
    }

    public void SetFacingDirection(int side)
    {
        FacingDirection = side;
    }
    public void Flip()
    {
        FacingDirection *= -1;
        Rb.transform.Rotate(0, 180, 0);
    }

    public void Flip180()
    {
        FacingDirection = 1;
        Rb.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void Flip0()
    {
        FacingDirection = -1;
        Rb.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
