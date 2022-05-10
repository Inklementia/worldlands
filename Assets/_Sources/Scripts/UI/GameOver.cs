using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Sources.Scripts.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private CanvasGroup gameOverCanvasGroup;
        [SerializeField] private TMP_Text text;
        
        public Transform PlayerDeathPoint { get; private set; }
        private void Awake()
        {
            gameOverCanvasGroup.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            GameActions.Instance.OnPlayerKilled += ShowGameOver;
        }
        private void OnDisable()
        {
            GameActions.Instance.OnPlayerKilled -= ShowGameOver;
        }
        public void ShowGameOver( Transform deathPoint)
        {
            PlayerDeathPoint = deathPoint;
                
            gameOverCanvasGroup.gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.PrependInterval(1);
            sequence.Append(gameOverCanvasGroup.DOFade(1, 1f));
         
        }

        public void HideGameOver()
        {
            gameOverCanvasGroup.DOFade(0, 1f);
        }
    }
}