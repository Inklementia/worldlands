using System;
using _Sources.Scripts.Helpers;
using _Sources.Scripts.Input;
using UnityEngine;

namespace _Sources.Scripts.Items
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private Tag playerTag;
        [SerializeField] private Tag itemGeneratorTag;
       
        [SerializeField] private Sprite openChestSprite;
        [SerializeField] private Sprite closeChestSprite;
        
        private SpriteRenderer _sr;
        private ItemGenerator _itemGenerator;
        private bool _isOpen;
        
        private void OnEnable()
        {
            _isOpen = false;
            _itemGenerator = GetComponentInParent<ItemGenerator>();
            _sr = GetComponent<SpriteRenderer>();
            _sr.sprite = closeChestSprite;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.HasTag(playerTag) && !_isOpen)
            {
                _isOpen = true;
                _sr.sprite = openChestSprite;
                GameObject itemGO = _itemGenerator.GetWeapon();
                GameObject item = Instantiate(itemGO, transform.position, Quaternion.identity);
                item.transform.SetParent(null);
                item.name = itemGO.name;

                // mb animation
            }
        }
    }
}