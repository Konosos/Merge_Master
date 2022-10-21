using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class HovLazerAttack : IAttackable
    {
        public void Attack(CharacterAttack characterAttack, GameObject target)
        {
            int layerMask = 1 >> characterAttack.gameObject.layer;
            layerMask = ~layerMask;
            Vector3 direct = (target.transform.position - characterAttack.firePoint.position).normalized;
            GameObject bulletClone = Behaviour.Instantiate(characterAttack.bullet, characterAttack.firePoint.position, Quaternion.identity);
            bulletClone.transform.SetParent(MatchManager.Instance.transform);
            Hovl_Laser bulletScr = bulletClone.GetComponent<Hovl_Laser>();
            //bulletScr.SetInfor(direct, characterAttack.charController.characterStats.GetDamge(), layerMask, target.transform.position);

        }
    }
}