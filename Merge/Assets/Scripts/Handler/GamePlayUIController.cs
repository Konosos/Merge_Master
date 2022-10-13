using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void OnEnable()
        {
            MatchManager.OnMatchEnd += EndGameUiOpen;
        }

        private void OnDisable()
        {
            MatchManager.OnMatchEnd -= EndGameUiOpen;
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