using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

namespace MergeHero
{
    public class Electric : Laser
    {
        [SerializeField] protected LightningBoltScript lightningBoltScript;
        // Start is called before the first frame update
        void Start()
        {
            lightningBoltScript.StartPosition = transform.position;
            lightningBoltScript.EndPosition = transform.position;

            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.ELECTRIC_KEY, 0.3f);
            //Invoke("DestroyMySelf", 2f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(dir * Time.deltaTime * speed);
            Vector3 laserLength = lightningBoltScript.StartPosition - lightningBoltScript.EndPosition;
            if (laserLength.magnitude >= 40)
            {
                //lightningBoltScript.EndPosition = transform.position - 30 * dir;
                DestroyMySelf();
            }
            lightningBoltScript.StartPosition = transform.position;
        }
    }
}