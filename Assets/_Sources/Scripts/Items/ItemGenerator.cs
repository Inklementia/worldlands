using System;
using System.Collections.Generic;
using _Sources.Scripts.Battle;
using _Sources.Scripts.Enemies.State_Mashine;
using _Sources.Scripts.Helpers;
using _Sources.Scripts.Object_Pooler;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Sources.Scripts.Items
{
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private List<CollectableItem> items = new List<CollectableItem>();
        [SerializeField] private List<CollectableItem> potions = new List<CollectableItem>();

        [Range(0, 100)] [SerializeField]
        private int dropChance;
        [SerializeField] private Tag chestTag;
        private ObjectPooler _objectPooler;

        private void Awake()
        {
            _objectPooler = ObjectPooler.Instance;
            
            GameObject itemGO = GetWeapon();
            GameObject item = Instantiate(itemGO, transform.position, Quaternion.identity);
            item.name = itemGO.name;
        }

        private void OnEnable()
        {
            GameActions.Instance.OnSpawnerDestroyed += DropChest;
            GameActions.Instance.OnEnemyKilled += DropPotion;
        }

        private void OnDisable()
        {
            GameActions.Instance.OnSpawnerDestroyed -= DropChest;
            GameActions.Instance.OnEnemyKilled -= DropPotion;
        }

        private void DropChest(Transform spawnerTransform)
        {
            GameObject chest = _objectPooler.SpawnFromPool(chestTag,
                    spawnerTransform.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0),
                    Quaternion.identity);

                chest.transform.localScale = new Vector3(.5f, .5f, 1f);
                Sequence sequence3 = DOTween.Sequence();
                sequence3.Append(chest.transform.DOScale(new Vector3(1f, 1f, 1f), .2f));
                sequence3.Append(chest.transform.DOScale(new Vector3(.7f, .7f, .7f), .3f));
            
        }
        private void DropPotion(GameObject enemyGO)
        { 
            int chance =  Random.Range(0, 101);
            
            if (chance <= dropChance)
            {
                Vector3 pos =new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0); 
                if (enemyGO.GetComponent<Entity>() != null)
                {
                   pos = enemyGO.GetComponent<Entity>().Dead.transform.position +
                                  new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
                }
                else
                {
                    pos = enemyGO.transform.position +
                                  new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
                }
             
            
                GameObject potion = Instantiate(DropPotion(), pos, Quaternion.identity);

                /*Sequence rotateSequence = DOTween.Sequence();
                rotateSequence.Append(potion.transform.DORotate(new Vector3(0, 0, -15f), .3f));
                rotateSequence.Append(potion.transform.DORotate(new Vector3(0, 0, 0f), .2f));
                */
                potion.transform.localScale = new Vector3(.5f, .5f, 1f);
                Sequence sequence3 = DOTween.Sequence();
                sequence3.Append(potion.transform.DOScale(new Vector3(1.1f,1.1f,1f), .2f));
                sequence3.Append(potion.transform.DOScale(new Vector3(1f,1f,1f), .3f));
            }
        }
        public GameObject GetWeapon()
        {
            int randomIndex = Random.Range(0, items.Count);
            var item = items[randomIndex];
            try
            {
                return item.prefab;
            }
            finally
            {
                if (item.isUnique)
                {
                    items.Remove(item);
                }
            }
        }
        public GameObject DropPotion()
        {
        
                int randomIndex = Random.Range(0, potions.Count);
                var potion = potions[randomIndex];
                return potion.prefab;
           
        }
        
    }
}