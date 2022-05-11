using _Sources.Scripts.Helpers;
using UnityEngine;

namespace _Sources.Scripts.UI
{
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
}
