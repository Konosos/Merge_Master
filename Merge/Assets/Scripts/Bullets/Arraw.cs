using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MergeHero
{
    public class Arraw : MonoBehaviour
    {
        private Vector3 dir;
        private Vector3 targetPos;
        private int dame;
        private CharacterType characterType;
        [SerializeField] private float speed;
        [SerializeField] private Transform parent;
        // Start is called before the first frame update
        void Start()
        {
            transform.LookAt(targetPos);
            Invoke("DestroyMySelf", 3f);
        }

        // Update is called once per frame
        void Update()
        {
            
            //parent.Translate(dir * Time.deltaTime * speed);
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
        }
        private void OnTriggerEnter(Collider other)
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
        void DestroyMySelf()
        {
            Destroy(gameObject);
        }
    }
}