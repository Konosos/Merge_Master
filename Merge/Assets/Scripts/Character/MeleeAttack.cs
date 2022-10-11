using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class MeleeAttack : CharacterAttack
    {
        public override void Attack(GameObject target)
        {
            base.Attack(target);

        }

        public MeleeAttack Clone() 
        {
            return this;
        }
    }
}