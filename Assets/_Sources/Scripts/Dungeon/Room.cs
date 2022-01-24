using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private int _height;
    private int _width;
    private BoxCollider2D _collider;
    private Vector2 _center;

    private List<Vector2> _tiles =  new List<Vector2>();

    private void Start()
    {
        
        _center = new Vector2(Mathf.Floor(_width/2), Mathf.Floor(_height/2));
        _collider = GetComponent<BoxCollider2D>();
    }

    public void SetRoomParameters(int width, int height)
    {
        _width = width;
        _height = height;
        _collider.size =  new Vector3(_width, _height);
    }

    public void AddTileToRoom(Vector2 tile)
    {
        _tiles.Add(tile);
    }
    
 
}
