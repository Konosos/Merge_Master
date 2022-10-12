using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private GameObject[,] heros = new GameObject[5, 7];


        [SerializeField] private GameObject chooseHero;

        [SerializeField]
        private GameObject meshCell;
        [SerializeField] private LayerMask character;

        private int curXBoard;
        private int curYBoard;

        public bool isStarted = false;
        private bool delayCheckMatch = false;

        private Touch initialTouch = new Touch();
        private bool hasSwiped = false;

        /*public List<HerosInChapter> herosInChapters;
        public HerosInChapter myHeros;
        public GameData curGameData;*/

        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            /*UIManager.instance.ShowMoney(curGameData.money);
            UIManager.instance.ShowArcherPrice(curGameData.priceArcher);
            UIManager.instance.ShowWarriorPrice(curGameData.priceWarrior);

            foreach (HeroData data in myHeros.heroDatas)
            {
                CreateHero(data.isPlayer, data.isWarrior, data.id, data.xBoard, data.yBoard);
            }
            foreach (HeroData data in herosInChapters[curGameData.curChapter].heroDatas)
            {
                CreateHero(data.isPlayer, data.isWarrior, data.id, data.xBoard, data.yBoard);
            }*/
            CreateChar(GameConfigs.BATMAN_NAME, 0, 3);
            CreateChar(GameConfigs.SPIDERMAN_NAME, 2, 0);
            CreateChar(GameConfigs.LONGLEGS_NAME, 2, 6);
            CreateChar(GameConfigs.BATMAN_NAME, 1, 3);
            CreateChar(GameConfigs.SPIDERMAN_NAME, 2, 1);
            CreateChar(GameConfigs.LONGLEGS_NAME, 3, 5);

        }

        private void CreateChar(string charName, int xBoard, int yBoard)
        {
            

            heros[xBoard, yBoard] = ChessCreater.Instance.CreateChessByName(charName, xBoard, yBoard);
        }

        // Update is called once per frame
        void Update()
        {
            if (isStarted)
            {
                if (!delayCheckMatch)
                {
                    //StartCoroutine(CheckMatchCoroutine());
                }
                return;
            }

            if (Input.touchCount > 0)
            {
                foreach (Touch t in Input.touches)
                {
                    if (t.phase == TouchPhase.Began)
                    {
                        initialTouch = t;
                        Ray ray = cam.ScreenPointToRay(initialTouch.position);
                        RaycastHit hit;
                        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, character))
                            return;
                        for (int i = 0; i < 7; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                Vector3 boardPos = new Vector3(-8f + 4 * j, 0, -12f + 4 * i);
                                Vector3 dir = hit.point - boardPos;
                                if (dir.magnitude > 2f)
                                    continue;
                                if (heros[j, i] != null)
                                {
                                    chooseHero = heros[j, i];
                                    chooseHero.GetComponent<CapsuleCollider>().isTrigger = true;
                                }
                            }
                        }
                    }
                    if (chooseHero == null)
                        return;
                    if (t.phase != TouchPhase.Ended && !hasSwiped)
                    {

                        Ray ray = cam.ScreenPointToRay(t.position);
                        RaycastHit hit;
                        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, character))
                            return;
                        chooseHero.transform.position = hit.point + Vector3.up;
                        for (int i = 0; i < 7; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                Vector3 boardPos = new Vector3(-8f + 4 * j, 0, -12f + 4 * i);
                                Vector3 dir = hit.point - boardPos;
                                if (dir.magnitude > 2f)
                                    continue;
                                curXBoard = j;
                                curYBoard = i;
                                meshCell.transform.position = new Vector3(-8f + 4 * curXBoard, 0.55f, -12f + 4 * curYBoard);
                            }
                        }
                    }
                    if (t.phase == TouchPhase.Ended)
                    {
                        meshCell.transform.position = new Vector3(0, -1f, 0);
                        chooseHero.GetComponent<CapsuleCollider>().isTrigger = false;
                        CharacterStats charInfor = chooseHero.GetComponent<CharacterStats>();
                        if (heros[curXBoard, curYBoard] == null)
                        {
                            SetPosEmty(charInfor.xBoard, charInfor.yBoard);
                            charInfor.SetBoardPos(curXBoard, curYBoard);
                            SetObjToBoard(chooseHero);
                        }
                        else
                        {
                            GameObject curHero = heros[curXBoard, curYBoard];
                            CharacterStats curHeroInfor = curHero.GetComponent<CharacterStats>();
                            if (curHeroInfor.characterType == charInfor.characterType && curHeroInfor.combatType == charInfor.combatType && curHeroInfor.charName == charInfor.charName && curHero != chooseHero)
                            {
                                string nextLvName = ChessCreater.Instance.GetNameOfNextLecel(curHeroInfor.charName);
                                if (nextLvName == null)
                                {
                                    curHeroInfor.SetBoardPos(curHeroInfor.xBoard, curHeroInfor.yBoard);
                                    charInfor.SetBoardPos(charInfor.xBoard, charInfor.yBoard);
                                    
                                }
                                else
                                {
                                    CreateChar(nextLvName, curHeroInfor.xBoard, curHeroInfor.yBoard);
                                    SetPosEmty(charInfor.xBoard, charInfor.yBoard);
                                    Destroy(chooseHero);
                                    Destroy(curHero);
                                }
                                
                            }
                            else
                            {
                                curHeroInfor.SetBoardPos(charInfor.xBoard, charInfor.yBoard);
                                charInfor.SetBoardPos(curXBoard, curYBoard);
                                SetObjToBoard(chooseHero);
                                SetObjToBoard(curHero);
                            }
                        }
                        chooseHero = null;
                        initialTouch = new Touch();
                        hasSwiped = false;
                    }
                }

            }
        }

        private void SetObjToBoard(GameObject obj)
        {
            CharacterStats charInfor = obj.GetComponent<CharacterStats>();
            heros[charInfor.xBoard, charInfor.yBoard] = obj;
        }
        private void SetPosEmty(int _xBoard, int _yBoard)
        {
            heros[_xBoard, _yBoard] = null;
        }
        /*private IEnumerator CheckMatchCoroutine()
        {
            delayCheckMatch = true;
            yield return new WaitForSeconds(1f);
            CheckMatch();
            delayCheckMatch = false;
        }
        private  void CheckMatch()
        {
            if(heros.Length<=0)
            {
                Debug.Log("WTF");
                return;
            }
            bool playerLose = true;
            bool enemyLose = true;
            foreach(GameObject hero in heros )
            {
                if (hero == null)
                    continue;
                if(hero.tag=="Player")
                {
                    playerLose = false;
                }
                else
                {
                    enemyLose = false;
                }
            }

            if(enemyLose)
            {
                Win();
                return;
            }
            if (playerLose)
            {
                Lose();
            }
        }
        private void Win()
        {
            curGameData.curChapter++;
            UIManager.instance.ShowEndMatchPan("You Win");
            Time.timeScale = 0f;
        }
        private void Lose()
        {
            UIManager.instance.ShowEndMatchPan("You Lose");
            Time.timeScale = 0f;
        }
        public void StartBut()
        {
            SaveMyHerosData();
            isStarted = true;
            UIManager.instance.IsShowButPan(false);

        }

        private void SaveMyHerosData()
        {
            myHeros.heroDatas = new List<HeroData>();
            for(int j=0;j<3;j++)
            {
                for(int i=0;i<5;i++)
                {
                    if (heros[i, j] == null)
                        continue;
                    if (heros[i, j].tag != "Player")
                        continue;
                    CharacterInformation charInfor = heros[i, j].GetComponent<CharacterInformation>();
                    bool _isWarrior = heros[i, j].layer == 8;
                    myHeros.heroDatas.Add(new HeroData {isPlayer=true, isWarrior=_isWarrior, id=charInfor.id, xBoard=charInfor.xBoard, yBoard=charInfor.yBoard });
                }
            }
        }

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