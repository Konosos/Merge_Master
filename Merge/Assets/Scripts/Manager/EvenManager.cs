using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MergeHero
{
    public class EvenManager : MonoBehaviour
    {
        #region Singleton
        public static EvenManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        public static System.Action OnHeroWarriorSpawn;
        public static System.Action OnHeroArcherSpawn;
        public static System.Action OnGameStarted;

    }
}