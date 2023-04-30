using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Sources.Scripts.Data;
using _Sources.Scripts.Helpers;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using _Sources.Scripts.Infrastructure.Services.SaveLoad;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using _Sources.Scripts.Weapons;
using DG.Tweening;
using Unity.Mathematics;
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
        [SerializeField] private GameObject endScreenGO;
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
                Debug.Log("Level "+_wm.CurrentLevel);
                if (_wm.CurrentLevel < 3)
                {
                    _wm.IncreaseLevel();
                    _saveLoadService.SaveProgress();

                    
                    var player = GameObject.FindWithTag("Player");
                    
                    ES3DataManager.Instance.SavePlayerHealthState(player.GetComponent<PlayerEntity>().Core.HealthSystem.CurrentStat);
                    ES3DataManager.Instance.SavePlayerEnergyState(player.GetComponent<PlayerEntity>().Core.EnergySystem.CurrentStat);
                    
                    levelTransfer.RunLevel("Main");
                }
                else
                {
                    GameObject panel = GameObject.FindWithTag("EndScreen").transform.GetChild(0).gameObject;
                    panel.SetActive(true);
                    //panel.GetComponent<CanvasGroup>().DOFade(1, .5f);
                    
                    Time.timeScale = 0;
                    ES3DataManager.Instance.DeleteEnergy();
                    ES3DataManager.Instance.DeleteHealth();
                    ES3DataManager.Instance.DeleteLevelNumber();
                    PlayerPrefs.DeleteAll();
                    
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