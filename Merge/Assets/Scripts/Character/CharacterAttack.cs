using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class CharacterAttack : MonoBehaviour , IAttackable
    {
        protected float coolDown;
        public IAttackable attackable;

        void Start()
        {

            attackable = AttackFactory.Create(CombatType.Range).CreateAttack(RangeAttackType.Batman);
            attackable.Attack(null);
        }
        public virtual void Attack(GameObject target)
        {
            
        }
    }
}