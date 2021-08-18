using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOld : ProjectileOld
{
    private float _travelDistance;
    private float _lifeDuration;
    private float _dragMultiplier;

    private float _speed;
    private Vector2 _direction;
    private bool _slowDown;
    private float _xStartPos;
    private float _maxDrag = 30;
    private float _lifeDurationTimer = 0.0f;

    private void Start()
    {
        _slowDown = false;

        rb.drag = 0;
        _xStartPos = transform.position.x;
        _lifeDurationTimer = 0.0f;

        rb.velocity = _direction * _speed;
    }
    private void FixedUpdate()
    {
        _lifeDurationTimer += Time.deltaTime;

        if (Mathf.Abs(_xStartPos - transform.position.x) >= _travelDistance && !_slowDown)
        {
            _slowDown = true;
            //rb.velocity = rb.velocity / Time.deltaTime;
            StartCoroutine(AddDrag());
        }

        if(_lifeDurationTimer >= _lifeDuration)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator AddDrag()
    {
        float current_drag = 0;
        while(current_drag < _maxDrag)
        {
            current_drag += Time.deltaTime * _dragMultiplier;
            rb.drag = current_drag;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.drag = 0;
    }

    public override void FireProjectile(float speed, Vector2 direction, float travelDistance, float lifeDuration, float dragMultiplier)
    {
        _speed = speed;
        _direction = direction;
        _travelDistance = travelDistance;
        _lifeDuration = lifeDuration;
        _dragMultiplier = dragMultiplier;
    }
}
