using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MergeHero
{
    public class GameStartUIHandler : MonoBehaviour
    {
        [Header("Canvas Group")]
        [SerializeField] CanvasGroup canvasGroup;
        [Header("Animation Variables")]
        [SerializeField] float fadeInDur;
        [SerializeField] float fadeOutDur;


        void Start()
        {

        }
        public void TurnOn()
        {
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            canvasGroup.DOFade(1, fadeInDur);

        }
        public void TurnOff()
        {

            canvasGroup.DOFade(0, fadeOutDur).onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        public void StartBtn()
        {
            
            GamePlayUIController.Instance.GameStartUiClose();
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.FIGHT_KEY, 0.7f);

            if (EvenManager.OnGameStarted != null)
            {
                EvenManager.OnGameStarted.Invoke();
            }

            GameManager.Instance.MatchStarted();
        }

        public void AddMoneyBtn()
        {
            GameManager.Instance.AddCoin(1000);
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }

        
    }
}