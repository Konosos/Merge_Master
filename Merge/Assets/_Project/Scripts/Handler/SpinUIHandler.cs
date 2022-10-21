using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace MergeHero
{
    public class SpinUIHandler : MonoBehaviour
    {
        [Header("Canvas Group")]
        [SerializeField] CanvasGroup canvasGroup;
        [Header("Animation Variables")]
        [SerializeField] float fadeInDur;
        [SerializeField] float fadeOutDur;

        [SerializeField] private RectTransform wheelRect;
        [SerializeField] private RectTransform light;


        [SerializeField] private Ease easy;

        [SerializeField] private SpinReward[] spinRewards;

        private bool canSpin = false;
        private bool spinning = false;
        [SerializeField] private Text idleTimeTxt;
        [SerializeField] private GameObject adsSpin;


        void Start()
        {

        }
        public void TurnOn()
        {
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            canvasGroup.DOFade(1, fadeInDur);
            SetSpinReward();
            StartCoroutine(IdleTimeCoroutine());
            light.DOLocalRotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }

        private void SetSpinReward()
        {
            foreach (SpinReward spin in spinRewards)
            {
                light.localEulerAngles = Vector3.zero;
                light.DOKill();
                spin.moneyTxt.text = spin.moneyReward.ToString();
            }
        }

        public void TurnOff()
        {
            canvasGroup.DOFade(0, fadeOutDur).onComplete += () =>
            {
                StopCoroutine(IdleTimeCoroutine());
                gameObject.SetActive(false);

            };
        }

        public void SpinBtn()
        {
            if (spinning)
                return;
            if (!canSpin)
            {
                //AdsController.Instance.ShowRewardedAd(OnSpin, null, "OnSpin");
                return;
            }

            canSpin = false;
            GameManager.Instance.LastSpinTime = (ulong)DateTime.Now.Ticks;

            OnSpin();


        }

        private void OnSpin()
        {
            spinning = true;
            int rand = UnityEngine.Random.Range(20, 30);
            int rewardIndex = rand % 10;
            int cur_Rag = 0;
            int i = 0;
            DOTween.To(() => cur_Rag, x => cur_Rag = x, rand * 36 + 18, 4f).SetEase(easy).OnUpdate(() =>
            {
                wheelRect.localEulerAngles = new Vector3(0, 0, cur_Rag);
                if (cur_Rag >= i * 36 + 18)
                {
                    i++;
                    //SoundManager.Instance.PlaySFX(SoundManager.Instance.click, "Tick2", 0.5f);
                }

            }).OnComplete(() =>
            {
                GameManager.Instance.AddCoin(spinRewards[rewardIndex].moneyReward);
                GamePlayUIController.Instance.AddCoins(Vector3.zero, 7, null);
                spinning = false;
            });
        }

        private IEnumerator IdleTimeCoroutine()
        {
            while (true)
            {

                int idleTime_Second = 86400 - (int)(((ulong)DateTime.Now.Ticks - GameManager.Instance.LastSpinTime) / TimeSpan.TicksPerSecond);

                if (idleTime_Second <= 0)
                {
                    canSpin = true;
                    adsSpin.SetActive(false);
                    idleTimeTxt.text = SecondToTime(0);
                }
                else
                {
                    adsSpin.SetActive(true);
                    idleTimeTxt.text = SecondToTime(idleTime_Second);
                }
                yield return new WaitForSeconds(1f);
            }

        }

        private string SecondToTime(int second)
        {
            string r = "";
            //HOURS
            r += (second / 3600).ToString("00") + ":";
            second -= second / 3600 * 3600;
            //MINUTES
            r += (second / 60).ToString("00") + ":";
            //SECONDS
            r += (second % 60).ToString("00");

            return r;
        }
    }
}