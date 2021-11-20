using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D Rb;
    [SerializeField] protected Tag PartcilesTag;
    [SerializeField] protected Tag TargetTag;

    protected GameObject[] Targets;
    protected float DamageAmount;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.HasTag(TargetTag))
        {
            // TODO: instaciate particles
            ObjectPooler.Instance.SpawnFromPool(PartcilesTag, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            // decsreasing health if projectile health target, and Tag matches target Tag
            collision.gameObject.GetComponentInParent<HealthSystem>().DecreaseHealth(DamageAmount);
            // turn off projectile
            gameObject.SetActive(false);

        }
    }

}
