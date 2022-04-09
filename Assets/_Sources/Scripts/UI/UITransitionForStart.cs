using System;
using DG.Tweening;
using UnityEngine;

namespace _Sources.Scripts.UI
{
    public class UITransitionForStart : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvas;
       

        private void Awake()
        {
            canvas.alpha = 0;
        }

        public void ShowGameUI()
        {
            canvas.DOFade(1, 1.3f);
        }
    }
}