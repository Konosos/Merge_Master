using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public interface IAttackable
    {
        void Attack(CharacterAttack characterAttack, GameObject target);
    }
}