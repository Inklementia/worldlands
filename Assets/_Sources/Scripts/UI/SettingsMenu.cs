using _Sources.Scripts.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _Sources.Scripts.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

        private float _originalMusicVolume;
        private float _originalSFXVolume;
        
        private const string SFXVolume = "SFXVolume";
        private const string MusicVolume = "MusicVolume";
        
        private void Start()
        {
            audioMixer.GetFloat(SFXVolume, out _originalMusicVolume);
            audioMixer.GetFloat(MusicVolume, out _originalSFXVolume);
        }


        // to turn off bg music
        public void SetBgMusicVolume(bool isBgOff)
        {
            //float originalVolume = _audioMixer.GetFloat("BgVolume");
            if (isBgOff)
            {
                audioMixer.SetFloat(MusicVolume, -80f);
            }
            else
            {
                audioMixer.SetFloat(MusicVolume, 0);
            }
       
        }
        public void SetSFXVolume(bool isOff)
        {
            //float originalVolume = _audioMixer.GetFloat("BgVolume");
            if (isOff)
            {
                audioMixer.SetFloat(SFXVolume, -80f);
            }
            else
            {
                audioMixer.SetFloat(SFXVolume, 0);
            }
       
        }

    }
}