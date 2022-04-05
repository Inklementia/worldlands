using System;
using System.Collections;
using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using _Sources.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Sources.Scripts.Dungeon 
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class ExitDoor : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private BoxCollider2D collider;
        [SerializeField] private LevelTransfer levelTransfer;
        [SerializeField] private Tag playerTag;
        [SerializeField] private string NextLevel;
        private ISaveLoadService _saveLoadService;

        [SerializeField] private Tag worldManagerTag;
        private WorldManager _wm;
        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
           
        }
        
        private void Reset()
        {
            rb.isKinematic = true;
            collider.isTrigger = true;
            collider.size = Vector2.one * 0.1f;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.HasTag(playerTag))
            {
                if (_wm.CurrentLevel < 3)
                {
                    _wm.IncreaseLevel();
                    
                    _saveLoadService.SaveProgress();
                    levelTransfer.RunLevel("Main");
                }
                  
                

            }
        }

        public void FindWorldManager()
        {
            _wm = GameObject.FindWithTag("WorldManager").GetComponent<WorldManager>();
        }
        private IEnumerator RestartDungeon()
        {
            yield return new WaitForSeconds(1f);
            levelTransfer.RunLevel("Main");
        }
   
    }
}