using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    
    public class MatchManager : MonoBehaviour
    {
        #region Singleton
        public static MatchManager Instance;

        private void Awake()
        {
            Instance = this;
        }
        #endregion

        [SerializeField] private Camera cam;
        [SerializeField] private GameObject[,] heros = new GameObject[5, 7];
        public List<CharacterStats> herosInMatch;
        public List<CharacterStats> monstersInMatch;


        [SerializeField] private GameObject chooseHero;

        [SerializeField] private GameObject meshCell;

        [SerializeField] private LayerMask character;

        private int curXBoard;
        private int curYBoard;

        private Touch initialTouch = new Touch();
        private bool hasSwiped = false;
        public static System.Action OnMatchEnd;

        private void Start()
        {
            /*CreateChar(GameConfigs.BATMAN_NAME, 0, 2);
            CreateChar(GameConfigs.SPIDERMAN_NAME, 2, 0);
            
            CreateChar(GameConfigs.BATMAN_NAME, 1, 2);
            CreateChar(GameConfigs.SPIDERMAN_NAME, 2, 1);*/
            //CreateChar(GameConfigs.LONGLEGS_NAME, 2, 6);
            //CreateChar(GameConfigs.LONGLEGS_NAME, 3, 5);
            int totalPlayerpower = 0;
            UserData userData = GameManager.Instance.UserData;
            for (int i = 0; i < userData.chessNames.Length; i++)
            {
                GameObject hero = CreateChar(userData.chessNames[i], userData.chess_XBoard[i], userData.chess_YBoard[i]);
                totalPlayerpower += ChessCreater.Instance.NameToPower(userData.chessNames[i]);

                //herosInMatch.Add(hero.GetComponent<CharacterStats>());
            }

            SpawnMonster((int)(totalPlayerpower * 1.3f));
        }

        private void Update()
        {
            if (GameManager.Instance.isStarted)
            {
                /*if (!delayCheckMatch)
                {
                    //StartCoroutine(CheckMatchCoroutine());
                }*/
                return;
            }
            #region Put and Merge
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
                                    StartCoroutine(charInfor.DestroyMe());
                                    StartCoroutine(curHeroInfor.DestroyMe());
                                    //Destroy(chooseHero);
                                    //Destroy(curHero);
                                    
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
            #endregion
        }

        private GameObject CreateChar(string charName, int xBoard, int yBoard)
        {
            heros[xBoard, yBoard] = ChessCreater.Instance.CreateChessByName(charName, xBoard, yBoard);

            heros[xBoard, yBoard].transform.SetParent(this.transform);

            return heros[xBoard, yBoard];
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

        private void SpawnMonster(int playerPower)
        {
            List<int> powerLists = RandomSpawnMonster(playerPower);
            int numMelee = 0;
            int numRange = 0;

            if(powerLists.Count % 2 == 0)
            {
                numMelee = powerLists.Count / 2;
                numRange = powerLists.Count / 2;
            }
            else
            {
                numMelee = powerLists.Count / 2;
                numRange = powerLists.Count / 2 + 1;
            }

            List<int> meleePowerLists = new List<int>();
            List<int> rangePowerLists = new List<int>();

            for(int i = 0; i < numMelee; i++)
            {
                meleePowerLists.Add(powerLists[i]);
            }

            for(int i= 0; i< numRange; i++)
            {
                rangePowerLists.Add(powerLists[powerLists.Count - 1 - i]);
            }
            int meleeX = 4;
            int meleeY = 4;
            for (int i = 0; i < meleePowerLists.Count; i++)
            {
                CreateChar(ChessCreater.Instance.PowerToName(CharacterType.Monster, CombatType.Melee, meleePowerLists[i]), meleeX, meleeY);
                meleeX--;
                if(meleeX < 0)
                {
                    meleeX = 4;
                    meleeY++;
                }
            }

            int rangeX = 0;
            int rangeY = 6;
            for (int i = 0; i < rangePowerLists.Count; i++)
            {
                CreateChar(ChessCreater.Instance.PowerToName(CharacterType.Monster, CombatType.Range, rangePowerLists[i]), rangeX, rangeY);
                rangeX++;
                if (rangeX > 4)
                {
                    rangeX = 0;
                    rangeY--;
                }
            }

        }

        

        public List<int> RandomSpawnMonster(int playerPower)
        {
            List<int> powerLists = new List<int>();
            int[] heroPowers = { 100, 200, 400/*, 800, 1600*/ };
            int maxCell = 8;
            int minCell = playerPower / heroPowers[heroPowers.Length - 1] + 1;
            if(minCell > maxCell)
            {
                minCell = maxCell;
            }

            int randomCell = Random.Range(minCell, maxCell);

            while(playerPower > 0 && randomCell > 0)
            {
                float offerPower = playerPower / randomCell;
                int offerHero = 0;
                float curDistance = 10000;
                foreach(int heroPower in heroPowers)
                {
                    if(Mathf.Abs(heroPower - offerPower) < curDistance)
                    {
                        offerHero = heroPower;
                        curDistance = Mathf.Abs(heroPower - offerPower);
                    }
                }

                powerLists.Add(offerHero);
                playerPower -= offerHero;
                randomCell--;
            }

            return powerLists;
        }

        public bool IsMonsterAllDie()
        {
            if (GameManager.Instance.matchEnd)
                return false;
            bool monstersAllDie = true;
            for(int i = 0; i < monstersInMatch.Count; i++)
            {
                if(monstersInMatch[i].isDeath == false)
                {
                    monstersAllDie = false;
                    break;
                }
            }

            if (monstersAllDie)
            {
                GameManager.Instance.IsPlayerWinner(true);
                
                if (OnMatchEnd != null)
                {
                    
                    OnMatchEnd.Invoke();
                }
            }

            return monstersAllDie;
        }

        public bool IsHeroAllDie()
        {
            if (GameManager.Instance.matchEnd)
                return false;
            bool herosAllDie = true;
            for (int i = 0; i < herosInMatch.Count; i++)
            {
                if (herosInMatch[i].isDeath == false)
                {
                    herosAllDie = false;
                    break;
                }
            }

            if (herosAllDie)
            {
                GameManager.Instance.IsPlayerWinner(false);
                
                if (OnMatchEnd != null)
                {

                    OnMatchEnd.Invoke();
                }
            }

            return herosAllDie;
        }

        public void SellWarrior()
        {
            for (int j = 2; j >= 0; j--)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (heros[i, j] == null)
                    {
                        CreateChar(GameConfigs.CAPTAIN_NAME, i, j);
                        goto go;
                    }
                }
            }
            go:;
        }

        public void SellArcher()
        {
            for (int j = 0; j <= 2; j++)
            {
                for (int i = 4; i >= 0; i--)
                {
                    if (heros[i, j] == null)
                    {
                        CreateChar(GameConfigs.SPIDERMAN_NAME, i, j);
                        goto go;
                    }
                }
            }
            go:;
        }

    }
}