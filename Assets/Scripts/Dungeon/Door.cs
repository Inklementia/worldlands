using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    Left,
    Right, 
    Top, 
    Bottom
}

public class Door : MonoBehaviour
{

    public DoorType DoorType;
    private float offset = 2f;
    // [SerializeField] private BoxCollider2D doorCollider;


    private GameObject _player;

   
    // substitude mb with entering point

    private void Start()
    {
        // to FIX It
        _player = GameObject.FindGameObjectWithTag("Player");
    }


   



}