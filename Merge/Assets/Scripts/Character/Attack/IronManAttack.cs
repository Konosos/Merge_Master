using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class IronManAttack : IAttackable
    {
        public void Attack(CharacterAttack characterAttack, GameObject target)
        {

            Vector3 direct = (target.transform.position - characterAttack.firePoint.position).normalized;
            GameObject bulletClone = Behaviour.Instantiate(characterAttack.bullet, characterAttack.firePoint.position, Quaternion.identity);
            Laser bulletScr = bulletClone.GetComponent<Laser>();
            bulletScr.SetInfor(direct, characterAttack.charController.characterStats.GetDamge(), characterAttack.charController.characterStats.characterType);


        }
    }
}