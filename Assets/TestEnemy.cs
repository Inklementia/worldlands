using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public Seeker Seeker { get; private set; }
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private float radius;
    public float NextWayPointDistance = 2f;


    public Path _path;
    public int _currentWayPoint = 0;
    public bool _reachedEndOfPath = false;
    [SerializeField] private Transform MoveTarget;
    private void Start()
    {
        Seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 2f);
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _path = path;
            _currentWayPoint = 0;
            Debug.Log("We Have Path");
        }
    }

    private void UpdatePath()
    {
        if (Seeker.IsDone())
        {
            //Debug.Log("Update Path working");
            Seeker.StartPath(Rb.position, MoveTarget.position, OnPathComplete);
            //MoveTarget.position = PickRandomPoint();
        }
    }

    private void FixedUpdate()
    {
        if (_path == null)
        {
            Debug.Log("Pathh null");
            return;
        }
        if (_currentWayPoint >= _path.vectorPath.Count)
        {
            Debug.Log("reached end TRUE");
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            Debug.Log("reached end False");
            _reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWayPoint] - Rb.position).normalized;
        Vector2 force = direction * 400 * Time.deltaTime;

        Rb.velocity = direction * 400 * Time.deltaTime;

        //Rb.AddForce(force);

        float distance = Vector2.Distance(Rb.position, _path.vectorPath[_currentWayPoint]);
        //Debug.Log("Distance " + distance);
        if (distance < NextWayPointDistance)
        {
            _currentWayPoint++;
        }
    }
    private Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * radius;

        point.y = 0;
        point += transform.position;
        return point;
    }
}
