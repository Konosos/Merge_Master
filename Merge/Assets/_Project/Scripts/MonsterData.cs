using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    [System.Serializable]
    public class MonsterData 
    {
        public MonsterName monsterName;
        public int xBoard;
        public int yBoard;

        
    }


    public enum MonsterName { Red , Whitty , Blue , Kissy , Huggy , Orange , CartoonCat, LongLegs , Green, Scp }
}