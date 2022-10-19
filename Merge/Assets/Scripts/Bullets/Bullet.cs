using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class Bullet : MonoBehaviour
    {
        protected Vector3 dir;
        protected int dame;
        protected CharacterType characterType;
        [SerializeField] protected float speed;
        // Start is called before the first frame update
        void Start()
        {
            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.THROW_KEY, 0.6f);
            Invoke("DestroyMySelf", 3f);
        }

        // Update is called once per frame
        protected void Update()
        {
            transform.Translate(dir * Time.deltaTime * speed);
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