using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public GameStartUIHandler gameStartUIHandler;
        public EndGameUIHandler endGameUIHandler;

        [SerializeField] private Text moneyTxt;

        private void Start()
        {
            OnMoneyChange(GameManager.Instance.PlayerMoney);
        }
        private void OnMoneyChange(int newMoneyValue)
        {
            moneyTxt.text = GameUtility.SimpleMoneyText(newMoneyValue);
        }
        

        private void OnEnable()
        {
            MatchManager.OnMatchEnd += EndGameUiOpen;
            GameManager.OnMoneyChange += OnMoneyChange;
        }

        private void OnDisable()
        {
            MatchManager.OnMatchEnd -= EndGameUiOpen;
            GameManager.OnMoneyChange -= OnMoneyChange;
        }

        public void GameStartUiClose()
        {
            gameStartUIHandler.TurnOff();
        }

        public void EndGameUiOpen()
        {
            endGameUIHandler.TurnOn();
        }
    }
}