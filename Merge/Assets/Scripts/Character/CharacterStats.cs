using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class CharacterStats : MonoBehaviour , ITakeDamege
    {
        protected int health;
        protected int damege;
        public string charName;

        public CharacterType characterType;
        public CombatType combatType;
        [SerializeField] protected Transform modelParent;

        public int xBoard;
        public int yBoard;

        public void SetUpStats(int setHealth, int setDamege, string setName, CharacterType setCharacterType, CombatType setCombatType)
        {
            health = setHealth;
            damege = setDamege;
            charName = setName;
            characterType = setCharacterType;
            combatType = setCombatType;
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
            
        }
    }
}