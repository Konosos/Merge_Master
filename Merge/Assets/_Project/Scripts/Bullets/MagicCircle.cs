using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class MagicCircle : Arraw
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.LookAt(targetPos);
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.MAGIC_CIRCLE_KEY, 0.6f);
            Invoke("DestroyMySelf", 3f);
        }


    }
}