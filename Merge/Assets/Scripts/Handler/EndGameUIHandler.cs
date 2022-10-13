using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MergeHero
{
    public class EndGameUIHandler : MonoBehaviour
    {
        [Header("Canvas Group")]
        [SerializeField] CanvasGroup canvasGroup;
        [Header("Animation Variables")]
        [SerializeField] float fadeInDur;
        [SerializeField] float fadeOutDur;

        [SerializeField] private Text log;

        void Start()
        {

        }
        public void TurnOn()
        {
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            canvasGroup.DOFade(1, fadeInDur);
            StartCoroutine(OnTurnOn());

        }
        public void TurnOff()
        {

            canvasGroup.DOFade(0, fadeOutDur).onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        public void NewGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator OnTurnOn()
        {
            yield return null;
            log.text = GameManager.Instance.playerIsWinner ? "Hero is winner" : "Monster is winner";
        }
    }
}