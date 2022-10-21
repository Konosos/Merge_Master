using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace MergeHero
{
    public class EndGameUIHandler : MonoBehaviour
    {
        [Header("Canvas Group")]
        [SerializeField] CanvasGroup canvasGroup;
        [Header("Animation Variables")]
        [SerializeField] float fadeInDur;
        [SerializeField] float fadeOutDur;

        [SerializeField] private Text log;

        [SerializeField] private Text rewardEndGameTxt;
        [SerializeField] private Text rewardWatchTxt;

        [SerializeField] private GameObject skipBtn;


        public RectTransform vectorRect;

        [SerializeField] private RectTransform[] txtObj;
        private int id;

        private bool wasRewarded = false;
        private int reward;
        private int index = 1;

        private bool vectorActive = false;
        private float curX;
        

        private int m_curIndex;
        public int Cur_Index
        {
            get { return m_curIndex; }
            set
            {
                if (value != m_curIndex && m_curIndex != 0)
                {
                    NewIndex(value);
                }
                m_curIndex = value;
                rewardWatchTxt.text = (reward * m_curIndex).ToString();
            }
        }

        private void NewIndex(int value)
        {
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.VECTOR_RUN_KEY, 0.2f);

            txtObj[id].localScale = Vector3.one;

            if (value == 2)
            {
                id = curX > 0 ? 4 : 0;

            }
            else if (value == 3)
            {
                id = curX > 0 ? 3 : 1;
            }
            else if(value == 5)
            {
                id = 2;
            }
            txtObj[id].DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() =>
            {
                
            });
        }

        public static System.Action OnCoinAnimComplete;

        private void OnEnable()
        {

            OnCoinAnimComplete += OnCoinEnd;
        }

        private void OnDisable()
        {

            OnCoinAnimComplete -= OnCoinEnd;
        }

        private void OnCoinEnd()
        {
            GameManager.Instance.ResetData();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        void Start()
        {

        }

        private void Update()
        {
            if (!vectorActive)
                return;
            curX = vectorRect.anchoredPosition.x;
            float ratate = Mathf.Abs(curX);
            if (ratate > 175)
            {
                Cur_Index = 2;

            }
            else if (ratate <= 175 && ratate > 60f)
            {
                Cur_Index = 3;

            }
            else if (ratate <= 60f)
            {
                Cur_Index = 5;

            }
        }
        public void TurnOn()
        {
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            canvasGroup.DOFade(1, fadeInDur);
            StartCoroutine(OnTurnOn());

        }
        public void TurnOff()
        {

            canvasGroup.DOFade(0, fadeOutDur).onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        public void NewGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }

        private IEnumerator OnTurnOn()
        {
            wasRewarded = false;


            reward = GameManager.Instance.playerIsWinner ? 100 : 50;
            rewardEndGameTxt.text = reward.ToString();

            vectorRect.DOAnchorPosX(250, 2f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
            vectorActive = true;
            yield return null;
            log.text = GameManager.Instance.playerIsWinner ? "Hero is winner" : "Monster is winner";

            string endGameSound = GameManager.Instance.playerIsWinner ? GameConfigs.VICTORY_KEY : GameConfigs.LOSE_KEY;
            SoundManager.Instance.PlaySFXByPublicSource(endGameSound, 1f);

            yield return new WaitForSeconds(5f);

            skipBtn.SetActive(true);
        }


        public void OnBtnRewardBonusClick()
        {
            if (wasRewarded)
                return;
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
            float ratate = Mathf.Abs(vectorRect.anchoredPosition.x);
            if (ratate > 175)
            {
                index = 2;
            }
            else if (ratate <= 175 && ratate > 60f)
            {
                index = 3;
            }
            else if (ratate <= 60f)
            {
                index = 5;
            }
            vectorRect.DOKill();
            vectorActive = false;
            wasRewarded = true;
            //AdsController.Instance.ShowRewardedAd(OnSuccessRewardBonus, null, "OnGiveRewardEndGame");
            OnSuccessRewardBonus();
        }

        public void OnSuccessRewardBonus()
        {
            GamePlayUIController.Instance.AddCoins(rewardWatchTxt.gameObject.transform.position, 7, OnCoinAnimComplete);
            GameManager.Instance.PlayerMoney += reward * index;
        }



        public void ClaimBtn()
        {

            if (wasRewarded)
                return;
            wasRewarded = true;
            vectorRect.DOKill();
            vectorActive = false;
            GameManager.Instance.PlayerMoney += reward * index;

            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
            //AdsController.Instance.ShowInterstitial();
            GamePlayUIController.Instance.AddCoins(rewardEndGameTxt.gameObject.transform.position, 7, OnCoinAnimComplete);
        }
    }
}