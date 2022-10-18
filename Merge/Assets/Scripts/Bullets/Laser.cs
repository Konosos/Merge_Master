using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class Laser : MonoBehaviour
    {
        protected Vector3 dir;
        protected int dame;
        protected CharacterType characterType;
        [SerializeField] protected float speed;
        [SerializeField] protected LineRenderer lineRenderer;
        // Start is called before the first frame update
        void Start()
        {
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.SetPosition(0, transform.position);
            Invoke("DestroyMySelf", 2f);
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.LASER_KEY, 0.4f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(dir * Time.deltaTime * speed);
            Vector3 laserLength = lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1);
            if (laserLength.magnitude >= 30)
            {
                Vector3 endPos = transform.position - 30 * dir;
                lineRenderer.SetPosition(1, endPos);
            }
            lineRenderer.SetPosition(0, transform.position);
        }
        protected void OnTriggerEnter(Collider other)
        {
            CharacterStats charInfor = other.GetComponent<CharacterStats>();
            if (characterType == charInfor.characterType)
                return;
            if (charInfor.isDeath)
                return;
            charInfor.TakeDamege(dame);
            Destroy(gameObject);
        }

        public void SetInfor(Vector3 setDir, int setDame, CharacterType setCharType)
        {
            dir = setDir;
            dame = setDame;
            characterType = setCharType;
        }
        protected void DestroyMySelf()
        {
            Destroy(gameObject);
        }
    }
}