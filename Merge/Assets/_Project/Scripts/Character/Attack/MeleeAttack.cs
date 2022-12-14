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
            characterStats.TakeDamege(characterAttack.charController.characterStats.GetDamge());
            if (characterAttack.hitPartical != null)
            {
                GameObject hit = Behaviour.Instantiate(characterAttack.hitPartical);
                Vector3 vfxPos = Vector3.Lerp(characterAttack.firePoint.position, target.transform.position, 0.7f);
                hit.transform.position = vfxPos;
            }
            int randId = Random.Range(0, 2);
            if(randId != 0)
            {
                SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.PUNCH2_KEY, 1f);
            }
            else
            {
                SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.PUNCH_KEY, 1f);
            }
            

        }

    }
}