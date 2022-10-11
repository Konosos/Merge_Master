using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
public class CharacterSO : ScriptableObject
{
    public int health;
    public int damege;
    public string nameChar;
    public GameObject prefab;
    public CharacterType characterType;
    public CombatType combatType;
    public HeroMeleeType heroMeleeType;
    public HeroRangeType heroRangeType;
    public MonsterMeleeType monsterMeleeType;
    public MonsterRangeType monsterRangeType;
    public RangeAttackType rangeAttackType;

}

public enum CharacterType { Hero, Monster }
public enum CombatType { Melee, Range}
public enum HeroMeleeType { None, Hulk, Captain, Thor}
public enum HeroRangeType { None, Batman, SpiderMan, GreenArrow }
public enum MonsterMeleeType { None }
public enum MonsterRangeType { None }
public enum RangeAttackType { None, Batman, SpiderMan, GreenArrow }

