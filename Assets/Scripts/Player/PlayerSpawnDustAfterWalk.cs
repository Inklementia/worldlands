using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnDustAfterWalk : PlayerAction
{
    [SerializeField] private float spawnDustInterval;

    [SerializeField] private GameObject dustGO;
    [SerializeField] private Animator dustAnim;
    [SerializeField] private Transform dustSpawnPoint;

    private float dustTimer = 0.0f;

    private void Update()
    {
        if (inputHandler._isPlayerMoving)
        {
            SpawnDust();
        }
        else
        {
            //check if dustGO is active
            if (dustGO.activeSelf)
            {
                dustAnim.SetTrigger("stop");
            }

        }
    }

    private void SpawnDust()
    {
        dustTimer += Time.deltaTime;

        if (!dustGO.activeSelf && dustTimer >= spawnDustInterval)
        {
            dustGO.transform.position = new Vector2(
              dustSpawnPoint.transform.position.x - (inputHandler._movementPosX),
              dustSpawnPoint.transform.position.y - (inputHandler._movementPosY)
            );

            dustTimer = 0.0f;
            dustGO.SetActive(true);
        }
    }

}
