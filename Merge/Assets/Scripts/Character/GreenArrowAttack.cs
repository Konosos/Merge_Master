using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class GreenArrowAttack : IAttackable
    {
        public  void Attack(GameObject target)
        {
            //base.Attack(target);
            Debug.Log("Arrow");
        }
    }
}