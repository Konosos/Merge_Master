using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MergeHero
{
    public class MeleeAttackFactory : AttackFactory
    {
        public override IAttackable CreateAttack(RangeAttackType rangeAttackType)
        {
            return new MeleeAttack();
            
        }
    }
}