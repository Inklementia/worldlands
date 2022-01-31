using System;
using System.Collections;
using UnityEngine;

namespace _Sources.Scripts.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeOutSpeed = 0.03f;
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            //gameObject.SetActive(false);
            StartCoroutine(FadeOut());
        }
        
        private IEnumerator FadeOut()
        {
           
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= fadeOutSpeed;
                yield return new WaitForSeconds(fadeOutSpeed);
            }
            
            gameObject.SetActive(false);
        }
    }
}