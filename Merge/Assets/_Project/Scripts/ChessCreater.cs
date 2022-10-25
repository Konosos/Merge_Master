using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MergeHero
{
    public class ChessCreater : MonoBehaviour
    {
        public CharacterSO[] characterSOs;
        [SerializeField] private GameObject charPrefab;

        public static ChessCreater Instance;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            //CreateChessByName("Batman", Vector3.up);
            //CreateChessByName("SpiderMan", new Vector3(2, 1, 2));
            //CreateChessByName("Thor", new Vector3(-2, 1, -2));
        }

        public GameObject CreateChessByName(string nameChar, int xBoard, int yBoard)
        {
            Vector3 pos = new Vector3(-8 + 4 * xBoard, 1.5f, -12 + 4 * yBoard);

            CharacterSO characterSO = Array.Find(characterSOs, character => character.nameChar == nameChar);
            GameObject charClone = Instantiate(charPrefab, pos, Quaternion.identity);
            CharacterStats charStats = charClone.GetComponent<CharacterStats>();
            if (characterSO == null)
            {
                LogUtils.Log(nameChar);
            }
            charStats.SetUpStats(characterSO.health, characterSO.damege, characterSO.nameChar, characterSO.characterType, characterSO.combatType, characterSO.power);
            charStats.SetBoardPos(xBoard, yBoard);

            GameObject model = Instantiate(characterSO.prefab, Vector3.zero, Quaternion.identity);
            charStats.SetModel(model);
            ModelInfor modelInfor = model.GetComponent<ModelInfor>();
            

            CharacterAttack charAttack = charClone.GetComponent<CharacterAttack>();
            charAttack.attackable = AttackFactory.Create(characterSO.combatType).CreateAttack(characterSO.rangeAttackType);

            charAttack.attackRange = characterSO.attackRange;
            charAttack.attackRate = characterSO.attackRate;
            charAttack.bullet = characterSO.bullet;
            charAttack.hitPartical = characterSO.hitPartical;

            charAttack.firePoint = modelInfor.firePoint;

            modelInfor.animationControllerListener.characterAttack = charAttack;

            CharacterAnimation charAnimation = charClone.GetComponent<CharacterAnimation>();
            charAnimation.animator = modelInfor.animator;

            return charClone;
        }

        public string PowerToName(CharacterType characterType, CombatType combatType, int power)
        {
            CharacterSO characterSO = Array.Find(characterSOs, character => character.power == power && character.characterType == characterType && character.combatType == combatType);
            if(characterSO != null)
            {

                return characterSO.nameChar;
            }
            else
            {
                LogUtils.Log("Not character with power = " + power);
                return null;
            }

        }

        public int NameToPower(string name)
        {
            CharacterSO characterSO = Array.Find(characterSOs, character => character.nameChar == name);
            if (characterSO != null)
            {
                return characterSO.power;
            }
            else
            {
                LogUtils.Log("Not character with name = " + name);
                return 0;
            }
        }

        public string GetNameOfNextLevel(string charName)
        {
            CharacterSO characterSO = Array.Find(characterSOs, character => character.nameChar == charName);
            CharacterSO nextLevelSO = null;

            if (characterSO.characterType == CharacterType.Hero)
            {
                switch (characterSO.combatType)
                {
                    case CombatType.Melee:
                        HeroMeleeType heroMeleeNextLevelType = characterSO.heroMeleeType + 1;
                        nextLevelSO = Array.Find(characterSOs, character => character.heroMeleeType == heroMeleeNextLevelType);
                        break;
                    case CombatType.Range:
                        HeroRangeType heroRangeNextLevelType = characterSO.heroRangeType + 1;
                        nextLevelSO = Array.Find(characterSOs, character => character.heroRangeType == heroRangeNextLevelType);
                        break;
                }
            }
            if (nextLevelSO == null)
            {
                LogUtils.Log("No ");
                return null;
            }
                
            return nextLevelSO.nameChar;
        }

        public int GetLevel(string charName)
        {
            CharacterSO characterSO = Array.Find(characterSOs, character => character.nameChar == charName);

            if (characterSO.characterType == CharacterType.Hero)
            {
                switch (characterSO.combatType)
                {
                    case CombatType.Melee:
                        return (int)characterSO.heroMeleeType;
                    case CombatType.Range:
                        return (int)characterSO.heroRangeType;
                }
            }
            else
            {
                switch (characterSO.combatType)
                {
                    case CombatType.Melee:
                        return (int)characterSO.monsterMeleeType;
                    case CombatType.Range:
                        return (int)characterSO.monsterRangeType;
                }
            }
            LogUtils.Log("Bug in GetLevel");
            return 0;
        }

        public string NameToSoundKey(string charName)
        {
            switch (charName)
            {
                case "HaileyQuin":
                    return GameConfigs.FEMALE_FALLING_KEY;
            }

            return GameConfigs.MALE_FALLING_KEY;
        }
    }
}