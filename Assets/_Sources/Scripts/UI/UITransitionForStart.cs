using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Sources.Scripts.UI
{
    public class UITransitionForStart : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvas;
        [SerializeField] private CanvasGroup canvasLevelText;

        private void Awake()
        {
            canvas.alpha = 0;
            canvasLevelText.alpha = 1;
        }

        public void ShowGameUI()
        {
            canvas.DOFade(1, 1.3f);
            canvasLevelText.DOFade(0, 1f);
        }
    }
}