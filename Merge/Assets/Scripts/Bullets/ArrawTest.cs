using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class ArrawTest : Arraw
    {
        [SerializeField] private float timeToMove = 1;
        private float curTime = 0;
        private Vector3 startPos;
        
        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
            Invoke("DestroyMySelf", 3f);
        }

        // Update is called once per frame
        void Update()
        {
            curTime += (Time.deltaTime / timeToMove);

            if (curTime > 1)
                return;

            transform.position = MathParabola.Parabola(startPos, targetPos, 3f, curTime);
        }
    }
}