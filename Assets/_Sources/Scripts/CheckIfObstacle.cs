using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfObstacle : MonoBehaviour
{
    public bool HasObstacleOnWay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HasObstacleOnWay = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        HasObstacleOnWay = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        HasObstacleOnWay = false;
    }
}
