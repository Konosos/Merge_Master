using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;
using System;

namespace MergeHero
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource sortSource;
        public AudioSource longSource;
        private bool playLoopSound;

        private bool m_SoundOn;
        public bool SoundOn
        {
            get { return m_SoundOn; }
            set
            {
                m_SoundOn = value;

                GameManager.Instance.UserData.soundOn = m_SoundOn;
                GameUtils.SavePlayerData(GameManager.Instance.UserData);
            }
        }
        private bool m_VibrateOn;
        public bool VibrateOn
        {
            get { return m_VibrateOn; }
            set
            {
                m_VibrateOn = value;

                GameManager.Instance.UserData.vibrateOn = m_VibrateOn;
                GameUtils.SavePlayerData(GameManager.Instance.UserData);
            }
        }

        public static SoundManager Instance;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SoundOn = GameManager.Instance.UserData.soundOn;
            VibrateOn = GameManager.Instance.UserData.vibrateOn;
        }
        public IEnumerator PlayLoopSound(string name, float volume, float delayTime)
        {
            if (!SoundOn)
                yield break ;
            playLoopSound = true;
            while (playLoopSound)
            {
                PlaySFX(longSource, name, volume);
                yield return new WaitForSeconds(delayTime);
            }
            

            
        }

        public void StopLoopSound()
        {
            playLoopSound = false;
            longSource.Stop();
        }

        public Sound[] sounds;

        public void PlaySFX(AudioSource audioSource, string name, float volumeScale)
        {
            if (!SoundOn)
                return;
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s != null)
            {
                audioSource.PlayOneShot(s.clip, volumeScale);
            }
        }

        public void PlaySFXByPublicSource(string name, float volumeScale)
        {
            PlaySFX(sortSource, name, volumeScale);
        }
    }
}