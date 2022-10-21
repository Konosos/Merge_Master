using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public abstract class AttackFactory
    {
        public abstract IAttackable CreateAttack(RangeAttackType rangeAttackType);
        public static AttackFactory Create(CombatType combatType)
        {
            
            switch (combatType)
            {
                case CombatType.Melee:
                    return new MeleeAttackFactory();

                case CombatType.Range:
                    return new RangeAttackFactory();


            }
            return null;
        }
    }
}