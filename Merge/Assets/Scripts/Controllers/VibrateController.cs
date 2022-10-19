using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeHero
{
    public class VibrateController : MonoBehaviour
    {
        //[SerializeField] Sprite[] vabrateStates;
        [SerializeField] GameObject[] vabrateStates;

        private bool m_Vabrate = true;
        public bool Vabrate
        {
            get { return m_Vabrate; }
            set
            {
                m_Vabrate = value;
                SetVabrateBtn();
                SoundManager.Instance.VibrateOn = m_Vabrate;
            }
        }

        private void Start()
        {
            Vabrate = GameManager.Instance.UserData.vibrateOn;
        }

        private void SetVabrateBtn()
        {
            if (Vabrate)
            {
                vabrateStates[0].SetActive(false);
                vabrateStates[1].SetActive(true);
            }
            else
            {
                vabrateStates[0].SetActive(true);
                vabrateStates[1].SetActive(false);
            }
        }

        public void OnClick()
        {
            Vabrate = !Vabrate;
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }
    }
}