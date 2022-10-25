using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField] private Text levelIndex;
        [SerializeField] private GameObject buyMeleeBtn;
        [SerializeField] private GameObject buyRangeBtn;
        [SerializeField] private GameObject fightBtn;
        [SerializeField] private GameObject handImg;

        void Start()
        {
            ShowLevelIndex();
        }
        public void TurnOn()
        {
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            canvasGroup.DOFade(1, fadeInDur);
            ShowLevelIndex();

        }
        public void TurnOff()
        {

            canvasGroup.DOFade(0, fadeOutDur).onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        private void ShowLevelIndex()
        {
            levelIndex.text = "Level : " + GameManager.Instance.GameIndex.ToString();
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
            TutorialManager.EndStep4.Invoke();
        }

        public void AddMoneyBtn()
        {
            GameManager.Instance.AddCoin(1000);
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }

        public void SetBuyMeleeBtn(bool isShow)
        {
            buyMeleeBtn.gameObject.SetActive(isShow);
            SetHand(isShow, buyMeleeBtn.transform.position);
        }

        public void SetBuyRangeBtn(bool isShow)
        {
            buyRangeBtn.gameObject.SetActive(isShow);
            SetHand(isShow, buyRangeBtn.transform.position);
        }

        public void SetFightBtn(bool isShow)
        {
            fightBtn.gameObject.SetActive(isShow);
            SetHand(isShow, fightBtn.transform.position);

        }

        private void SetHand(bool isShow, Vector3 pos)
        {
            handImg.gameObject.SetActive(isShow);

            Transform handTrans = handImg.transform;
            if (isShow)
            {
                handTrans.position = pos;
                handTrans.localScale = Vector3.one * 1.5f;
                handTrans.DOScale(Vector3.one * 1f, 1f).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                handTrans.DOKill();
            }
        }
    }
}