using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class ObjectPoolerManager : MonoBehaviour
    {
        #region Singleton
        public static ObjectPoolerManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;

        }

        public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
        public Queue<Pool> pools;

        [SerializeField] private GameObject moneyTxt;

        private void Start()
        {
            CreatePoolList();
            CreatePoolDictionary();
        }

        private void CreatePoolList()
        {
            pools = new Queue<Pool>();

            pools.Enqueue(new Pool { tag = "MoneyTxt", prefab = moneyTxt, size = 20 });
            
        }
        private void CreatePoolDictionary()
        {
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectList = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);

                    obj.SetActive(false);
                    objectList.Enqueue(obj);

                    obj.transform.SetParent(this.transform);
                }
                poolDictionary.Add(pool.tag, objectList);
            }
        }

        public GameObject GetObject(string key)
        {
            GameObject getObj = null;
            if (poolDictionary.ContainsKey(key))
            {
                getObj = poolDictionary[key].Dequeue();

                getObj.SetActive(true);
                poolDictionary[key].Enqueue(getObj);

            }
            return getObj;
        }
    }
}