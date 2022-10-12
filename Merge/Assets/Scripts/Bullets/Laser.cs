using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class Laser : MonoBehaviour
    {
        private Vector3 dir;
        private int dame;
        private CharacterType characterType;
        [SerializeField] private float speed;
        [SerializeField] private LineRenderer lineRenderer;
        // Start is called before the first frame update
        void Start()
        {
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.SetPosition(0, transform.position);
            Invoke("DestroyMySelf", 3f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(dir * Time.deltaTime * speed);
            lineRenderer.SetPosition(0, transform.position);
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

        public void SetInfor(Vector3 setDir, int setDame, CharacterType setCharType)
        {
            dir = setDir;
            dame = setDame;
            characterType = setCharType;
        }
        void DestroyMySelf()
        {
            Destroy(gameObject);
        }
    }
}