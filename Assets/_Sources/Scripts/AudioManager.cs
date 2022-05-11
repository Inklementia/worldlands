using System;
using _Sources.Scripts.Helpers;
using UnityEngine;

namespace _Sources.Scripts
{
    public class AudioManager : SingletonClass<AudioManager>
    {
        public Sound[] sounds;
        
        private void Awake()
        {
            //when game starts -> create all sound sources wil values
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = s.output;
                s.source.playOnAwake = s.playOnAwake;
            }
            Play("Bg");
        }

        // called from where specific sound is needed
        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if( s == null)
            { 
                Debug.LogWarning("Sound:" + name + " not found!");
                return;
            }
  
            s.source.Play();
        }
    }
}