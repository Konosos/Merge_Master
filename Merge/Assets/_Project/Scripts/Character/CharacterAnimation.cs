using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private CharController charController;

        private AnimStates state;
        public Animator animator;

        private void OnEnable()
        {
            MatchManager.OnMatchEnd += PlayAnimVictory;
        }

        private void OnDisable()
        {
            MatchManager.OnMatchEnd -= PlayAnimVictory;
        }

        private void PlayAnimVictory()
        {
            if (!charController.characterStats.isDeath)
            {
                charController.characterMovement.MoveIsStopped(true);
                Victory();
            }
        }

        public void Idle()
        {
            state = AnimStates.Idle;
            animator.SetInteger("State", (int)state);
        }

        public void Run()
        {
            state = AnimStates.Run;
            animator.SetInteger("State", (int)state);
        }

        public void Attack()
        {
            //state = AnimStates.Attack;
            //animator.SetInteger("States", (int)state);
            animator.SetTrigger("Attack");
        }

        public void Victory()
        {
            //state = AnimStates.Victory;
            //animator.SetInteger("State", (int)state);
            animator.SetTrigger("Victory");
        }
        public void Lose()
        {
            state = AnimStates.Lose;
            animator.SetInteger("State", (int)state);
        }

        public void Falling()
        {
            state = AnimStates.Falling;
            animator.SetInteger("State", (int)state);
        }

        public void Die()
        {

            animator.SetTrigger("Die");
        }
    }
}