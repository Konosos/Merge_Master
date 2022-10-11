using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class RangeAttackFactory : AttackFactory
    {
        public override IAttackable CreateAttack(RangeAttackType rangeAttackType)
        {
            switch (rangeAttackType)
            {
                case RangeAttackType.None:
                    return null;
                //break;
                case RangeAttackType.Batman:
                    return new BatmanAttack();
                    //break;
                case RangeAttackType.GreenArrow:
                    return new GreenArrowAttack();
                    //break;

            }
            return null;
        }
    }
}