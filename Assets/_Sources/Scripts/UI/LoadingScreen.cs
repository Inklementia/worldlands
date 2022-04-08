using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Sources.Scripts.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeOutSpeed = 0.03f;
        [SerializeField] private float additionalWaitTime = 1f;

        [SerializeField] private Material screenTransitionMaterial;
        [SerializeField] private string propertyName = "_Progress";
        [SerializeField] private float transitionTime = 1f;
        [SerializeField] private TMP_Text loadingText;
        public UnityEvent OnTransitionDone;
        private void Awake()
        {
            DontDestroyOnLoad(this);
            screenTransitionMaterial.SetFloat(propertyName, 0);
        }

        public void Show()
        {
           screenTransitionMaterial.SetFloat(propertyName, 0);
            gameObject.SetActive(true);
           
            //canvasGroup.alpha = 1;
        }

        public void ShowAnimation()
        {
     
            StartCoroutine(ScreenTransitionIn());
        }
        public void Hide()
        {
            //gameObject.SetActive(false);
            StartCoroutine(ScreenTransitionOut());
        }
        
        private IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(additionalWaitTime);
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= fadeOutSpeed;
                yield return new WaitForSeconds(fadeOutSpeed);
            }
            gameObject.SetActive(false);
        }

        private IEnumerator ScreenTransitionOut()
        {
            yield return new WaitForSeconds(additionalWaitTime);
            float currentTime = 0;
            while (currentTime < transitionTime)
            {
                currentTime += Time.deltaTime;
                loadingText.DOFade(0f, .1f);
                screenTransitionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));
              
                yield return null;
            }
            OnTransitionDone?.Invoke();
            gameObject.SetActive(false);
           
        }
        private IEnumerator ScreenTransitionIn()
        {
            gameObject.SetActive(true);
            float currentTime = transitionTime;
            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                loadingText.DOFade(1f, .1f);
                screenTransitionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));
              
                yield return null;
            }
            OnTransitionDone?.Invoke();
           
           
        }
    }
}