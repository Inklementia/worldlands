using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Enemies
{
    public class SpiderDent : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private SpriteRenderer sr;
        private int _currentLevel;

        private void Awake()
        {
            sr.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}