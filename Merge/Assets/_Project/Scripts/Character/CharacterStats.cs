using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace MergeHero
{   
    [SelectionBase]
    public class CharacterStats : MonoBehaviour , ITakeDamege
    {
        public CharController charController;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Collider coll;

        protected int health;
        protected int damege;
        public string charName;
        public int power;

        public CharacterType characterType;
        public CombatType combatType;
        [SerializeField] protected Transform modelParent;
        [SerializeField] protected HealthBar healthBar;

        public int xBoard;
        public int yBoard;
        public bool isDeath;

        void Start()
        {
            healthBar.SetMaxHealth(health);
            switch (characterType)
            {
                case CharacterType.Hero:
                    MatchManager.Instance.herosInMatch.Add(this);
                    GameManager.Instance.SavePlayerChess();
                    StartCoroutine(OnHeroSpawn());
                    break;
                case CharacterType.Monster:
                    MatchManager.Instance.monstersInMatch.Add(this);
                    break;
            }
        }

        private void OnEnable()
        {
            
            
        }

        private void OnDestroy()
        {
            switch (characterType)
            {
                case CharacterType.Hero:
                    MatchManager.Instance.herosInMatch.Remove(this);
                    
                    break;
                case CharacterType.Monster:
                    MatchManager.Instance.monstersInMatch.Remove(this);
                    break;
            }
        }

        private IEnumerator OnHeroSpawn()
        {
            yield return new WaitForEndOfFrame();
            GameManager.Instance.SavePlayerChess();
            if (EvenManager.OnHeroSpawn != null)
            {
                EvenManager.OnHeroSpawn.Invoke();
            }
            string[] purchasedHero = GameManager.Instance.UserData.purchasedHero;
            if (!purchasedHero.IsContain(charName))
            {
                if (EvenManager.OnPurchasedNewHero != null)
                {
                    GameManager.Instance.UserData.purchasedHero = TeraJet.GameUtils.AddItemToArray(GameManager.Instance.UserData.purchasedHero, charName);
                    TeraJet.GameUtils.SavePlayerData(GameManager.Instance.UserData);
                    EvenManager.OnPurchasedNewHero.Invoke(charName);
                }
            }

            if(GameManager.gameStates == GameManager.GameStates.Tutorial && charName == GameConfigs.BATMAN_NAME)
            {
                TutorialManager.EndStep3.Invoke();
            }
        }

        public IEnumerator DestroyMe()
        {
            Destroy(this.gameObject);
            yield return new WaitForEndOfFrame();
            GameManager.Instance.SavePlayerChess();
            
        }

        public void SetUpStats(int setHealth, int setDamege, string setName, CharacterType setCharacterType, CombatType setCombatType, int setPower)
        {
            health = setHealth;
            damege = setDamege;
            charName = setName;
            characterType = setCharacterType;
            combatType = setCombatType;
            power = setPower;
            if (characterType == CharacterType.Hero)
            {
                healthBar.SetColor(false);
            }
            if (characterType == CharacterType.Monster)
            {
                healthBar.SetColor(true);
                this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        public int GetDamge()
        {
            return damege;
        }

        public void SetModel(GameObject model)
        {
            model.transform.SetParent(modelParent);
            model.transform.localPosition = Vector3.zero;
            model.transform.localEulerAngles = Vector3.one;
            modelParent.localScale = Vector3.one * 0.1f;
            modelParent.DOScale(Vector3.one * (1.2f + (ChessCreater.Instance.GetLevel(charName) - 1) * 0.2f), 0.2f);
            //modelParent.localScale = Vector3.one * (1 + (ChessCreater.Instance.GetLevel(charName) - 1) * 0.2f);
        }

        public void SetBoardPos(int xPos, int yPos)
        {
            xBoard = xPos;
            yBoard = yPos;
            this.transform.position = new Vector3(-8 + 4 * xBoard, 1.5f, -12 + 4 * yBoard);
        }

        

        public void TakeDamege(int damege)
        {
            if (GameManager.Instance.matchEnd)
                return;
            if (isDeath)
                return;
            health -= damege;
            healthBar.SetHealth(health);
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.HURT_KEY, 0.8f);
            if (characterType == CharacterType.Monster)
            {
                GameManager.Instance.AddCoin(damege);
                AddMoneyTxt addMoneyTxt = ObjectPoolerManager.Instance.GetObject("MoneyTxt").GetComponent<AddMoneyTxt>();
                addMoneyTxt.SetUpAndFly(this.transform.position + Vector3.up * 4f, damege);
            }
            if (health <= 0)
            {
                isDeath = true;
                Death();
            }
        }

        private void Death()
        {
            
            //charController.characterAnimation.Victory();
            
            rb.isKinematic = true;
            coll.isTrigger = true;
            charController.characterMovement.MoveIsStopped(true);
            switch (characterType)
            {
                case CharacterType.Hero:
                    MatchManager.Instance.IsHeroAllDie();
                    SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.HERO_DEATH_KEY, 0.7f);
                    break;
                case CharacterType.Monster:
                    MatchManager.Instance.IsMonsterAllDie();

                    SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.MONSTERDEATH_KEY, 0.5f);
                    break;
            }
            charController.characterAnimation.Die();
            healthBar.gameObject.SetActive(false);
            //charController.characterMovement.GetAgent().enabled = false;
        }
    }
}
