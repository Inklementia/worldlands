using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Sources.Scripts
{
    public class DamagePopUp : MonoBehaviour
    {
        private TextMeshPro _textMesh;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;
        private void OnEnable()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(rectTransform.DOMoveY(rectTransform.position.y + 0.5f, 1f));
            sequence.AppendInterval(.5f);
            sequence.Insert(0, transform.DOScale(Vector3.zero, sequence.Duration()));
            sequence.Insert(0, canvasGroup.DOFade(0, sequence.Duration() - 1f)).OnComplete(DisableText);
            
            
        }

        private void DisableText()
        {
            transform.position = Vector3.zero;
            canvasGroup.alpha = 1;
            transform.localScale = Vector3.one;
            gameObject.SetActive(false);
           
        }
     

        private void Awake()
        {
            _textMesh = transform.GetComponent<TextMeshPro>();
          
        }

        public void SetUp(float damageAmount)
        {
            _textMesh.SetText(damageAmount.ToString());
        }
    }
}