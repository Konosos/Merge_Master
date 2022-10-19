using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class GreenArrowAttack : IAttackable
    {
        public  void Attack(CharacterAttack characterAttack, GameObject target)
        {
            Vector3 direct = (target.transform.position - characterAttack.firePoint.position).normalized;
            GameObject bulletClone = Behaviour.Instantiate(characterAttack.bullet, characterAttack.firePoint.position, Quaternion.identity);
            bulletClone.transform.SetParent(MatchManager.Instance.transform);
            Arraw bulletScr = bulletClone.GetComponentInChildren<Arraw>();
            bulletScr.SetInfor(direct, characterAttack.charController.characterStats.GetDamge(), characterAttack.charController.characterStats.characterType, target.transform.position);
        }
    }
}