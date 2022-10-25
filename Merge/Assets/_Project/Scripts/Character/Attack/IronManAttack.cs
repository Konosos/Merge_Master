using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class IronManAttack : IAttackable
    {
        public void Attack(CharacterAttack characterAttack, GameObject target)
        {
            Vector3 targetPos = target.transform.position + Vector3.up * 1.5f;

            Vector3 direct = (targetPos - characterAttack.firePoint.position).normalized;
            GameObject bulletClone = Behaviour.Instantiate(characterAttack.bullet, characterAttack.firePoint.position, Quaternion.identity);
            bulletClone.transform.SetParent(MatchManager.Instance.transform);
            Laser bulletScr = bulletClone.GetComponent<Laser>();
            bulletScr.SetInfor(direct, characterAttack.charController.characterStats.GetDamge(), characterAttack.charController.characterStats.characterType);


        }
    }
}