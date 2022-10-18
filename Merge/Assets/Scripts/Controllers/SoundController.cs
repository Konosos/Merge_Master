using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeHero
{
    public class SoundController : MonoBehaviour
    {
        //[SerializeField] Sprite[] soundStates;
        [SerializeField] GameObject[] soundStates;

        private bool m_Sound = true;
        public bool Sound
        {
            get { return m_Sound; }
            set
            {
                m_Sound = value;
                SetSoundBtn();
                SoundManager.Instance.SoundOn = m_Sound;

            }
        }

        private void Start()
        {
            Sound = GameManager.Instance.UserData.soundOn;

        }

        private void SetSoundBtn()
        {
            if (Sound)
            {
                soundStates[0].SetActive(false);
                soundStates[1].SetActive(true);
            }
            else
            {
                soundStates[0].SetActive(true);
                soundStates[1].SetActive(false);
            }
        }

        public void OnClick()
        {
            Sound = !Sound;
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }
    }
}