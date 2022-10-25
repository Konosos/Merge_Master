using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MergeHero
{

    [CreateAssetMenu(fileName = "NewLevelData", menuName = "Level_Data")]
    public class LevelData : ScriptableObject
    {
        public string key;
        public MonsterData[] monsters;
    }
}