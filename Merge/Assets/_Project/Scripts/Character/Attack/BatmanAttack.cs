using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class BatmanAttack : IAttackable
    {

        
        public  void Attack(CharacterAttack characterAttack, GameObject target)
        {
            //base.Attack(target);
            //CharacterStats characterStats = target.GetComponent<CharacterStats>();
            //characterStats.TakeDamege(5);
            Vector3 targetPos = target.transform.position + Vector3.up * 1.5f;

            Vector3 direct = (targetPos - characterAttack.firePoint.position).normalized;
            GameObject bulletClone = Behaviour.Instantiate(characterAttack.bullet, characterAttack.firePoint.position, Quaternion.identity);
            
            bulletClone.transform.SetParent(MatchManager.Instance.transform);
            Bullet bulletScr = bulletClone.GetComponent<Bullet>();
            bulletScr.SetInfor(direct, characterAttack.charController.characterStats.GetDamge(), characterAttack.charController.characterStats.characterType);

        }
    }
}