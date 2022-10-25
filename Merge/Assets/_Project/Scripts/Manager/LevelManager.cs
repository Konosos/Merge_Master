using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class LevelManager : MonoBehaviour
    {
        #region Sigleton
        public static LevelManager Instance;
        private void Awake()
        {
            Instance = this;

        }
        #endregion

        public LevelData[] levelDatas;

        private void Start()
        {
            
        }

        public LevelData GetLevelData(int levelIndex)
        {
            LevelData levelData = System.Array.Find(levelDatas, level => level.key == levelIndex.ToString());

            if(levelData == null)
            {
                LogUtils.Log("Bug in GetLevelData");
            }

            return levelData;
        }
    }
}