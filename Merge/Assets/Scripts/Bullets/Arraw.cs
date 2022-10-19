using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MergeHero
{
    public class Arraw : MonoBehaviour
    {
        protected Vector3 dir;
        protected Vector3 targetPos;
        protected int dame;
        protected CharacterType characterType;
        [SerializeField] protected float speed;
        [SerializeField] protected Transform parent;
        // Start is called before the first frame update
        void Start()
        {
            transform.LookAt(targetPos);
            Invoke("DestroyMySelf", 3f);
        }

        // Update is called once per frame
        protected void Update()
        {
            
            //parent.Translate(dir * Time.deltaTime * speed);
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
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

        public void SetInfor(Vector3 setDir, int setDame, CharacterType setCharType, Vector3 setTarget)
        {
            dir = setDir;
            dame = setDame;
            characterType = setCharType;
            targetPos = setTarget;
        }
        protected void DestroyMySelf()
        {
            Destroy(gameObject);
        }
    }
}