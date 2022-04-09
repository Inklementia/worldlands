using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Sources.Scripts.Data;
using _Sources.Scripts.Infrastructure.Services;
using _Sources.Scripts.Infrastructure.Services.PersistentProgress;
using _Sources.Scripts.Infrastructure.Services.SaveLoad;
using _Sources.Scripts.Player.PlayerFiniteStateMachine;
using _Sources.Scripts.Weapons;
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

                    var player = GameObject.FindWithTag("Player");
                    List<ShootingWeapon> weapons = player.GetComponent<PlayerWeaponry>().CarriedWeapons;

                
                    ES3DataManager.Instance.SaveEquipedWeapon(weapons[0].gameObject);
                    ES3DataManager.Instance.SaveSecondaryWeapon(weapons[1].gameObject);
                    
                    ES3DataManager.Instance.SavePlayerHealthState(player.GetComponent<PlayerEntity>().Core.HealthSystem.CurrentStat);
                    ES3DataManager.Instance.SavePlayerEnergyState(player.GetComponent<PlayerEntity>().Core.EnergySystem.CurrentStat);
                    
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