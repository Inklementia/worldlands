using System;
using _Sources.Scripts.Dungeon;
using UnityEngine;

namespace _Sources.Scripts.Player
{
    public class Defogger : MonoBehaviour
    {
        [SerializeField] private FogSO fogData;
        [SerializeField] private PlayerInputHandler playerInput;

        private void FixedUpdate()
        {
            if (playerInput.CkeckIfJoystickPressed())
            {
                Defog();
            }
        
        }

        private void Defog()
        {
            Vector2 rayDirection = Vector2.zero;
            int rayCount = 12;
            float angleIncrement = 360 / rayCount;
            for (int i = 0; i < rayCount; i++)
            {
                float wallDist = fogData.MaxVisibility + 5;
                float rotAngle = angleIncrement * i;

                Vector3 radiusPos = transform.position + Quaternion.AngleAxis(rotAngle, Vector3.forward) * transform.up;
                rayDirection = (transform.position - radiusPos).normalized;
                RaycastHit2D hitWall =
                    Physics2D.Raycast(transform.position, rayDirection, Mathf.Infinity, fogData.WhatIsWall);
                wallDist = Mathf.Min(hitWall.distance, fogData.MaxVisibility);
                RaycastHit2D[] hits =
                    Physics2D.RaycastAll(transform.position, rayDirection, wallDist, fogData.WhatIsFog);
                
                Debug.DrawRay(transform.position, rayDirection * wallDist, Color.yellow);
                foreach (RaycastHit2D hit in hits)
                {
                    hit.transform.GetComponent<Fog>().Defog();
                }
            }
        }
        
    }
}