using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MergeHero
{
    public class GamePlayUIController : MonoBehaviour
    {
        #region Singleton
        public static GamePlayUIController Instance;

        private void Awake()
        {
            Instance = this;
        }
        #endregion

        #region  Coin Anim
        //References
        [Header("UI references")]
        //[SerializeField] TMP_Text coinUIText;
        [SerializeField] GameObject animatedCoinPrefab;
        [SerializeField] Transform target;

        [Space]
        [Header("Available coins : (coins to pool)")]
        [SerializeField] int maxCoins;
        Queue<GameObject> coinsQueue = new Queue<GameObject>();

        [Space]
        [Header("Animation settings")]
        [SerializeField] [Range(0.2f, 0.5f)] float minAnimDuration;
        [SerializeField] [Range(0.5f, 1f)] float maxAnimDuration;

        [SerializeField] Ease easeType;
        [SerializeField] float spread;

        private void PrepareCoins()
        {
            GameObject coin;
            for (int i = 0; i < maxCoins; i++)
            {
                coin = Instantiate(animatedCoinPrefab);
                coin.transform.SetParent(transform, false);
                coin.SetActive(false);
                coinsQueue.Enqueue(coin);

            }
        }


        public void Animate(Vector3 collectedCoinPosition, int amount, System.Action OnAnimationComplete)
        {
            Transform[] coinTrans = new Transform[amount];

            for (int i = 0; i < amount; i++)
            {
                //check if there's coins in the pool
                if (coinsQueue.Count > 0)
                {


                    //extract a coin from the pool
                    GameObject coin = coinsQueue.Dequeue();
                    coin.SetActive(true);
                    coinTrans[i] = coin.transform;
                    //move coin to the collected coin pos
                    coinTrans[i].position = collectedCoinPosition;
                    Vector3 randomSpread = new Vector3(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread), 0f);

                    coinTrans[i].DOMove(collectedCoinPosition + randomSpread, 0.3f);

                }

            }
            //animate coin to target position
            StartCoroutine(CoinAnim(coinTrans, OnAnimationComplete));

        }
        private IEnumerator CoinAnim(Transform[] coinTrans, System.Action OnAnimationComplete)
        {
            yield return new WaitForSeconds(0.31f);
            foreach (Transform coin in coinTrans)
            {


                float duration = UnityEngine.Random.Range(minAnimDuration, maxAnimDuration);
                coin.DOMove(target.position, 0.35f).SetEase(easeType).OnComplete(() =>
                {
                    //executes whenever coin reach target position
                    coin.gameObject.SetActive(false);
                    coinsQueue.Enqueue(coin.gameObject);
                    SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.COLLECTMONEY, 0.75f);


                });
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(0.5f);
            if (OnAnimationComplete != null)
            {
                OnAnimationComplete.Invoke();
            }

        }

        public void AddCoins(Vector3 collectedCoinPosition, int amount, System.Action OnAnimationComplete)
        {
            Animate(collectedCoinPosition, amount, OnAnimationComplete);
        }
        #endregion

        [Space]
        public GameStartUIHandler gameStartUIHandler;
        public EndGameUIHandler endGameUIHandler;
        public PreviewNewHeroUIHandler previewNewHeroUIHandler;
        public SettingUIHandler settingUIHandler;
        public SpinUIHandler spinUIHandler;

        [SerializeField] private Text moneyTxt;

        private void Start()
        {
            OnMoneyChange(GameManager.Instance.PlayerMoney);
            PrepareCoins();
        }
        private void OnMoneyChange(int newMoneyValue)
        {
            moneyTxt.text = GameUtility.SimpleMoneyText(newMoneyValue);
        }
        

        private void OnEnable()
        {
            MatchManager.OnMatchEnd += EndGameUiOpen;
            GameManager.OnMoneyChange += OnMoneyChange;
            EvenManager.OnPurchasedNewHero += PreviewHeroUiOpen;
        }

        private void OnDisable()
        {
            MatchManager.OnMatchEnd -= EndGameUiOpen;
            GameManager.OnMoneyChange -= OnMoneyChange;
            EvenManager.OnPurchasedNewHero -= PreviewHeroUiOpen;
        }

        public void SpinUIOpen()
        {
            spinUIHandler.TurnOn();
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }

        public void SpinUIClose()
        {
            spinUIHandler.TurnOff();
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }

        public void SettingUiOpen()
        {
            if (settingUIHandler.isOpen)
            {
                settingUIHandler.isOpen = false;
                settingUIHandler.TurnOff();
            }
            else
            {
                settingUIHandler.isOpen = true;
                settingUIHandler.TurnOn();
            }
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }

        public void GameStartUiClose()
        {
            gameStartUIHandler.TurnOff();
        }

        public void EndGameUiOpen()
        {
            endGameUIHandler.TurnOn();
        }

        public void PreviewHeroUiOpen(string charName)
        {
            previewNewHeroUIHandler.TurnOn(charName);
        }

    }
}