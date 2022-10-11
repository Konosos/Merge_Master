using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class BatmanAttack : IAttackable
    {
        public GameObject a;
        
        public  void Attack(GameObject target)
        {
            //base.Attack(target);
            Debug.Log("Batman");
        }
    }
}