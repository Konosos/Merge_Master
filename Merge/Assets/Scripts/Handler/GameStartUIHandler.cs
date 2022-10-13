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
            GameManager.Instance.MatchStarted();
            GamePlayUIController.Instance.GameStartUiClose();
        }

        public void SellWarrior()
        {
            MatchManager.Instance.SellWarrior();
        }

        public void SellArcher()
        {
            MatchManager.Instance.SellArcher();
        }
    }
}