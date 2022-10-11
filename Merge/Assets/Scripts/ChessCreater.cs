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
            charStats.SetUpStats(characterSO.health, characterSO.damege, characterSO.name, characterSO.characterType, characterSO.combatType);
            charStats.SetBoardPos(xBoard, yBoard);
            GameObject model = Instantiate(characterSO.prefab, Vector3.zero, Quaternion.identity);
            charStats.SetModel(model);

            return charClone;
        }

        public string GetNameOfNextLecel(string charName)
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
                return null;
            return nextLevelSO.name;
        }
    }
}