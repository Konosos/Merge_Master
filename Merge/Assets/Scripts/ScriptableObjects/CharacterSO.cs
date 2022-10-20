using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
public class CharacterSO : ScriptableObject
{
    public int health;
    public int damege;
    public int power;
    public string nameChar;
    public float attackRate;
    public float attackRange;
    public GameObject prefab;
    public GameObject bullet;
    public Sprite avatar;
    public CharacterType characterType;
    public CombatType combatType;
    public HeroMeleeType heroMeleeType;
    public HeroRangeType heroRangeType;
    public MonsterMeleeType monsterMeleeType;
    public MonsterRangeType monsterRangeType;
    public RangeAttackType rangeAttackType;

    private void OnValidate()
    {
        switch (combatType)
        {
            case CombatType.Melee:
                attackRange = 2.7f;
                break;
            case CombatType.Range:
                attackRange = 1000f;
                break;
        }
    }
}

public enum CharacterType { Hero, Monster }
public enum CombatType { Melee, Range}
public enum HeroMeleeType { None, BlackPanther, Batman, HarleyQuinn , Thor, Captain }
public enum HeroRangeType { None, SpiderMan, GreenArrow, TheFlash, IronMan, DoctorStranger }
public enum MonsterMeleeType { None, Red, Whitty, Blue, Kissy, Huggy }
public enum MonsterRangeType { None, Orange, Green, LongLegs }
public enum RangeAttackType { None, SpiderMan, Batman, GreenArrow, IronMan }
public enum AnimStates { Idle, Run, Victory, Lose, Attack, Falling}


