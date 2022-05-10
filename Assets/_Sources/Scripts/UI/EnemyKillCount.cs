using System.Collections;
using System.Collections.Generic;
using _Sources.Scripts;
using UnityEngine;

public class EnemyKillCount : MonoBehaviour
{
    public static int score;

    private void Start()
    {
        score = 0;
        GameActions.Instance.OnEnemyKilled += AddKill;
        //GameActions.Current.OnBattleColliderEntered += DetectEnemies;
    }

    private void OnDisable()
    {
        GameActions.Instance.OnEnemyKilled -= AddKill;
        // GameActions.Current.OnBattleColliderEntered -= DetectEnemies;
        //GameActions.Current.OnDungeonGenerated -= DetectEnemies;
    }

    private void AddKill(GameObject obj)
    {
        score += 1;
    }

    private void ResetKills()
    {
        score = 0;
    }
}
