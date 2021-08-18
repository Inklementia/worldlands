using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileOld : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;


    public abstract void FireProjectile(float speed, Vector2 direction, float travelDistance, float lifeDuration, float dragMultiplier);
}
