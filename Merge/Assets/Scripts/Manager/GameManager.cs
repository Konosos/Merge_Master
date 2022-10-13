using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TeraJet;

namespace MergeHero
{
    public class GameManager : MonoBehaviour
    {
        

        public bool isStarted = false;
        public bool matchEnd = false;
        public bool playerIsWinner = false;
        public bool gameOver = false;

        #region Sigleton
        public static GameManager Instance;
        private void Awake()
        {
            IsPlayerDataLoaded = false;

            if (Instance == null)
            {
                Application.targetFrameRate = 300;
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                // Make sure that the server is connected

                // Start Loading the PlayerData
            }
            else if (Instance != this)
            {
                //Instance is not the same as the one we have, destroy old one, and reset to newest one
                Destroy(Instance.gameObject);
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        #endregion

        /*bool isShowBanner;
        public void ShowBanner()
        {
            if (!isShowBanner)
            {
                AdsController.Instance.ShowBanner();
                isShowBanner = true;
            }
        }
        public void HideBanner()
        {
            AdsController.Instance.HideBanner();
            isShowBanner = false;
        }*/

        private void Start()
        {

        }

        
        #region Load User data
        public event System.Action OnPlayerDataLoaded;

        [SerializeField] UserData m_PlayerData;
        public UserData UserData { get { return m_PlayerData; } set { m_PlayerData = value; } }

        bool m_IsDataLoaded;
        public bool IsPlayerDataLoaded { get { return m_IsDataLoaded; } set { m_IsDataLoaded = value; LoadData(); } }
        public int GameIndex
        {
            get { return m_PlayerData.gameIndex; }
            set
            {
                m_PlayerData.gameIndex = value;
                GameUtils.SavePlayerData(UserData);
            }
        }
        public string PlayerName
        {
            get { return m_PlayerData.playerName; }
            set
            {
                m_PlayerData.playerName = value;
                //player.characterName = m_PlayerData.playerName;
                GameUtils.SavePlayerData(UserData);
            }
        }
        public int PlayerMoney
        {
            get { return m_PlayerData.playerMoney; }
            set
            {
                m_PlayerData.playerMoney = value;
                GameUtils.SavePlayerData(UserData);
                if (OnMoneyChange != null)
                {
                    OnMoneyChange.Invoke(value);
                }
            }
        }

        public static System.Action<int> OnMoneyChange = delegate { };

        public int PlayerRank
        {
            get { return m_PlayerData.playerRanking; }
            set
            {
                m_PlayerData.playerRanking = value;

                GameUtils.SavePlayerData(UserData);
            }
        }

        public ulong LastSpinTime
        {
            get { return m_PlayerData.lastSpinTime; }
            set
            {
                m_PlayerData.lastSpinTime = value;
                GameUtils.SavePlayerData(UserData);
            }
        }
        public void LoadData()
        {
            if (!m_IsDataLoaded)
            {
                StartCoroutine(LoadUserData());
            }
            else
            {
                // When the data loaded => Syncronize all refs and Fire the event for others Instance
                //QualitySettings.SetQualityLevel(m_PlayerData._qualitySettingsIndex);
                if (OnPlayerDataLoaded != null)
                {
                    //LogUtils.Log("Fire event current playerDataLoaded !!!! ");
                    OnPlayerDataLoaded();
                }
            }
        }




        IEnumerator LoadUserData()
        {
            if (!IsPlayerDataLoaded)
            {
                m_PlayerData = GameUtils.LoadPlayerData();
            }
            while (m_PlayerData == null)
            {
                //LogUtils.Log("Waiting for Player Data! Show Loading... UI");

                yield return null;
            }
            //UpdateUserSettingsData();
            m_IsDataLoaded = true;

            // Fire PlayerDataLoaded Event for the other Instances Objects
            if (OnPlayerDataLoaded != null)
            {
                //LogUtils.Log("Fire event playerDataLoaded for other objects !!!! ");
                OnPlayerDataLoaded();
            }

        }
        #endregion
        
        #region Check mouse on UI element
        public bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        public static bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
        ///Returns 'true' if we touched or hovering on Unity UI element.
        public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            if (eventSystemRaysastResults.Count == 0)
            {
                return true;
            }
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    return true;
                }
                else
                {

                }

            }
            //Debug.Log(eventSystemRaysastResults.Count);
            return false;
        }
        ///Gets all event systen raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
        #endregion
        public void ResetData()
        {

            matchEnd = false;
            playerIsWinner = false;
            gameOver = false;
        }
        public string RandomString(int lenght)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string generated_string = "";

            for (int i = 0; i < lenght; i++)
                generated_string += characters[UnityEngine.Random.Range(0, characters.Length)];

            return generated_string;
        }

        public void AddCoin(int _coin)
        {
            PlayerMoney += _coin;

            if (PlayerMoney < 0) Debug.Log("Dont have money");
        }

        public void IsPlayerWinner(bool isWin)
        {
            playerIsWinner = isWin;
            matchEnd = true;
        }

        public void MatchStarted()
        {
            isStarted = true;
        }

        public void SavePlayerChess()
        {
            List<CharacterStats> heroList = MatchManager.Instance.herosInMatch;
            int numHero = heroList.Count;
            UserData.chessNames = new string[numHero];
            UserData.chess_XBoard = new int[numHero];
            UserData.chess_YBoard = new int[numHero];

            for (int i = 0; i < numHero; i++)
            {
                UserData.chessNames[i] = heroList[i].charName;
                UserData.chess_XBoard[i] = heroList[i].xBoard;
                UserData.chess_YBoard[i] = heroList[i].yBoard;
            }

            GameUtils.SavePlayerData(UserData);
        }
        /*
        

        public void SellWarriorBut()
        {
            if (curGameData.money < curGameData.priceWarrior)
                return;
            for (int j = 2; j >= 0; j--)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (heros[i, j] == null)
                    {
                        CreateHero(true, true, 1, i, j);
                        curGameData.money -= curGameData.priceWarrior;
                        curGameData.priceWarrior += 200;
                        UIManager.instance.ShowMoney(curGameData.money);
                        UIManager.instance.ShowWarriorPrice(curGameData.priceWarrior);
                        goto go;
                    }
                }
            }
        go:;
        }
        public void SellArcherBut()
        {
            if (curGameData.money < curGameData.priceArcher)
                return;
            for (int j = 0; j <= 2; j++)
            {
                for (int i = 4; i >= 0; i--)
                {
                    if (heros[i, j] == null)
                    {
                        CreateHero(true, false, 1, i, j);
                        curGameData.money -= curGameData.priceArcher;
                        curGameData.priceArcher += 200;
                        UIManager.instance.ShowMoney(curGameData.money);
                        UIManager.instance.ShowArcherPrice(curGameData.priceArcher);
                        goto go;
                    }
                }
            }
        go:;
        }*/
    }
}