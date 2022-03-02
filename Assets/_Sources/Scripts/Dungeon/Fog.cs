using System;
using UnityEngine;

namespace _Sources.Scripts.Dungeon
{
    public class Fog : MonoBehaviour
    {
        [SerializeField] private FogSO fogData;
        private bool _isDefogged;
        private Transform _player;
        private SpriteRenderer _sr;

        private void Start()
        {
            _player = gameObject.FindWithTag(fogData.PlayerTag).transform;
            _sr = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            ChangeOpacity();
        }

        public void Defog()
        {
            _isDefogged = true;
        }
        private void ChangeOpacity()
        {
            if (_isDefogged && _player != null)
            {
                float dist = Vector2.Distance(transform.position, _player.position);
                float lightRange = Mathf.Clamp(dist, 0, fogData.LightRadius);
                Color tmp = _sr.color;
                tmp.a = Mathf.Round(lightRange) * (fogData.MaxOpacity/ fogData.LightRadius); // not higher then .5f alpha
                _sr.color = tmp;
            }
        }
    }
}