using System;
using System.Collections;
using UnityEngine;

namespace _Sources.Scripts
{
    public class FadeOutSFX : MonoBehaviour
    {
        [SerializeField] private float _delay = 2f;
        [SerializeField] private float _fadeDelay = 1f;
        [SerializeField] private float _aliphaValue = 0;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private bool _destroy = true;

        private void OnEnable()
        {
            Color originalColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
            _spriteRenderer.color = originalColor;
        }

        private void Start()
        {
            
            
            StartCoroutine(FadeTo(_aliphaValue, _fadeDelay, _delay));
        }

        private IEnumerator FadeTo(float alphaValue, float fadeDelay, float delay)
        {
            yield return new WaitForSeconds(delay);
            // original alpha
            float alpha = _spriteRenderer.color.a;

            for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / fadeDelay)
            {
                Color newColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Mathf.Lerp(alpha, alphaValue, i));
                _spriteRenderer.color = newColor;
                yield return null;
            }

            if (_destroy)
            {
               
               gameObject.SetActive(false);
            }
        }
    }
}