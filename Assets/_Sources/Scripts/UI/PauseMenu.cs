using System;
using UnityEngine;

namespace _Sources.Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {

        private SettingsMenu _settingsMenu;
        private bool _isPaused;
        private int _switchState; // 0 pause-icon , 1 play-icon
    
        //private AudioManager _audioManager;
        
        private void Awake()
        {
            _settingsMenu = transform.GetComponent<SettingsMenu>();
            //_audioManager = FindObjectOfType<AudioManager>();
        }

        

        private void Update()
        {
            Time.timeScale = _isPaused ? 0 : 1;

        }
        public void TogglePause()
        {
            _isPaused = !_isPaused;
        }
    
        
        public void UnpauseGame()
        {
            _isPaused = false;
        }
        public void PauseGame()
        {
            _isPaused = true;
        }
    }
}