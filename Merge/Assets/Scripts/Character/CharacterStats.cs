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
            //charController.characterMovement.GetAgent().enabled = false;
        }
    }
}