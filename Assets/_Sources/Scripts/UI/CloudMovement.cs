using System;
using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts.Data;
using DG.Tweening;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] private RectTransform cloudImage;
    [SerializeField] private float shiftAmount;
    private void Start()
    {

    }

    private void FixedUpdate()
    {
        
        Vector2 pos = cloudImage.anchoredPosition;
        Vector2 movement = new Vector2(-SimpleInput.GetAxis("Horizontal") * shiftAmount, -SimpleInput.GetAxis("Vertical")*shiftAmount);

        //cloudImage.anchoredPosition = movement;
        cloudImage.DOAnchorPos(movement, 1);
    }
}
