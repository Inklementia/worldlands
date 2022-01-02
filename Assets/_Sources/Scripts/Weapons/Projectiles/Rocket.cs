using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rocket : Projectile
{
    private Transform _target;
    protected GameObject[] _targets;

    private float _speed;
    private float _rotationSpeed;
    private float _rotateAngleDeviation;

    private float _randomRotateAngleDeviation;

    private void Start()
    {
        _targets = gameObject.FindAllWithTag(TargetTag).ToArray();
        _target = GetClosestTarget(_targets);
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)_target.position - Rb.position;

        direction.Normalize();

        _randomRotateAngleDeviation = Random.Range(-_rotateAngleDeviation, _rotateAngleDeviation);
        float rotateAmount = Vector3.Cross(direction, transform.right).z + _randomRotateAngleDeviation;

        Rb.angularVelocity = -rotateAmount * _rotationSpeed;
        Rb.velocity = transform.right * _speed;
    }

    private Transform GetClosestTarget(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (var potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }

    public override void FireProjectile(
        float damage,
        float speed,
        float rotationSpeed,
        float rotationAngleDeviation,
        Vector2 direction,
        float travelDistance,
        float lifeDuration,
        float dragMultiplier
        )
    {
        DamageAmount = damage;
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        _rotateAngleDeviation = rotationAngleDeviation;
        
        AttackDetails.DamageAmount = damage;
        AttackDetails.Position = transform.position;
    }
}

