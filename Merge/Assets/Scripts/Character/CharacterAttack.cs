using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class CharacterAttack : MonoBehaviour , IAttackable
    {
        public CharController charController;

        protected float attackCountdown;
        public float attackRate;
        public float attackRange;
        public IAttackable attackable;
        public GameObject bullet;
        public GameObject enemy;
        public Transform firePoint;
        public CharacterStats enemyStats;
        [SerializeField] private LayerMask targetMask;

        void Start()
        {

            
            //attackable.Attack(this, null);
        }

        private void Update()
        {
            if(!GameManager.Instance.isStarted)
            {
                return;
            }
            if (charController.characterStats.isDeath)
                return;
            if (enemy == null)
            {
                FindEnemy();
            }
            else
            {
                if (enemyStats.isDeath)
                {
                    FindEnemy();
                    return;
                }
                
                if (EnemyInRangeAttack())
                {
                    charController.characterMovement.MoveIsStopped(true);
                    charController.characterAnimation.Idle();
                    transform.LookAt(enemy.transform.position);
                    if (attackCountdown <= 0f)
                    {
                        charController.characterAnimation.Attack();
                        attackCountdown = 1f / attackRate;
                    }

                    
                    
                }
                else
                {
                    charController.characterMovement.MoveIsStopped(false);
                    charController.characterMovement.MoveToPosition(enemy.transform.position);
                }
                attackCountdown -= Time.deltaTime;
            }
        }
        public virtual void Attack(CharacterAttack characterAttack, GameObject target)
        {
            
        }

        public bool EnemyInRangeAttack()
        {
            Vector3 directionToTarget = enemy.transform.position - transform.position;
            if (directionToTarget.magnitude <= attackRange)
                return true;
            return false;
        }

        public void FindEnemy()
        {

            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 100, targetMask);
            if (rangeChecks.Length == 0)
                return;
            //Debug.Log(rangeChecks.Length);
            float distance = 100000f;
            foreach (Collider obj in rangeChecks)
            {
                CharacterStats characterStats = obj.GetComponent<CharacterStats>();

                if (characterStats.characterType == charController.characterStats.characterType)
                    continue;
                if (characterStats.isDeath)
                    continue;
                Transform target = obj.transform;
                Vector3 directionToTarget = target.position - transform.position;

                if (directionToTarget.magnitude < distance)
                {
                    enemy = obj.gameObject;
                    enemyStats = characterStats;
                    distance = directionToTarget.magnitude;

                }

            }

        }
    }
}