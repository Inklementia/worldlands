using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D Rb;
    //[SerializeField] protected Tag PartcilesTag;
    [SerializeField] protected Tag TargetTag;

    [SerializeField] protected MMFeedback destroyParticlesFeedback;

    protected GameObject[] Targets;
    protected float DamageAmount;
    protected AttackDetails AttackDetails;

    protected ObjectPooler objectPooler;

    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // upon firing a projectile assign all needed info about its behaviur
    public virtual void FireProjectile(
        float damageAmount,
        float speed,
        float rotationSpeed,
        float rotationAngleDeviation,
        Vector2 direction,
        float travelDistance,
        float lifeDuration,
        float dragMultiplier
        )
    {
        DamageAmount = damageAmount;
        AttackDetails.DamageAmount = damageAmount;
        AttackDetails.Position = transform.position;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.HasTag(TargetTag))
        {
            // TODO: instantiate particles
           // destroyParticlesFeedback.PlayFeedbacks();
            //objectPooler.SpawnFromPool(PartcilesTag, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            // decreasing health if projectile health target, and Tag matches target Tag
            collision.gameObject.SendMessage("Damage", AttackDetails);
            // turn off projectile
            gameObject.SetActive(false);

        }
    }

}
