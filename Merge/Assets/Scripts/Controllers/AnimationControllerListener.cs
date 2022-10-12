using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MergeHero
{
    public class AnimationControllerListener : MonoBehaviour
    {
        public CharacterAttack characterAttack;
        
        public void OnPunchEnd()
        {
            if(characterAttack != null)
            {
                characterAttack.attackable.Attack(characterAttack, characterAttack.enemy);
            }
        }
    }
}