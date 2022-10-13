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
            Invoke("DestroyMySelf", 2f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(dir * Time.deltaTime * speed);
            Vector3 laserLength = lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1);
            if (laserLength.magnitude >= 40)
            {
                Vector3 endPos = transform.position - 40 * dir;
                lineRenderer.SetPosition(1, endPos);
            }
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