using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class MeleeAttack : IAttackable
    {
        public  void Attack(CharacterAttack characterAttack, GameObject target)
        {
            //base.Attack(target);
            CharacterStats characterStats = target.GetComponent<CharacterStats>();
            characterStats.TakeDamege(50);

        }

    }
}