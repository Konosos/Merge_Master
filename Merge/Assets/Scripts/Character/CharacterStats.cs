using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{   
    [SelectionBase]
    public class CharacterStats : MonoBehaviour , ITakeDamege
    {
        [SerializeField] private CharController charController;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Collider coll;

        protected int health;
        protected int damege;
        public string charName;

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

        public IEnumerator DestroyMe()
        {
            Destroy(this.gameObject);
            yield return new WaitForEndOfFrame();
            GameManager.Instance.SavePlayerChess();
        }

        public void SetUpStats(int setHealth, int setDamege, string setName, CharacterType setCharacterType, CombatType setCombatType)
        {
            health = setHealth;
            damege = setDamege;
            charName = setName;
            characterType = setCharacterType;
            combatType = setCombatType;
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
            if (health <= 0)
            {
                isDeath = true;
                Death();
            }
        }

        private void Death()
        {
            
            charController.characterAnimation.Victory();
            
            rb.isKinematic = true;
            coll.isTrigger = true;
            charController.characterMovement.MoveIsStopped(true);
            switch (characterType)
            {
                case CharacterType.Hero:
                    MatchManager.Instance.IsHeroAllDie();
                    break;
                case CharacterType.Monster:
                    MatchManager.Instance.IsMonsterAllDie();
                    break;
            }
            //charController.characterMovement.GetAgent().enabled = false;
        }
    }
}