using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MergeHero
{
    public class PreviewNewHeroUIHandler : MonoBehaviour
    {
        [Header("Canvas Group")]
        [SerializeField] CanvasGroup canvasGroup;
        [Header("Animation Variables")]
        [SerializeField] float fadeInDur;
        [SerializeField] float fadeOutDur;

        [SerializeField] private Image avatar;
        [SerializeField] private Text damegeTxt;
        [SerializeField] private Text healthTxt;

        void Start()
        {

        }
        public void TurnOn(string charName)
        {
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            canvasGroup.DOFade(1, fadeInDur);
            StartCoroutine(OnTurnOn(charName));

        }
        public void TurnOff()
        {

            canvasGroup.DOFade(0, fadeOutDur).onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        private IEnumerator OnTurnOn(string charName)
        {
            yield return null;
            CharacterSO characterSO = System.Array.Find(ChessCreater.Instance.characterSOs, character => character.nameChar == charName);
            avatar.sprite = characterSO.avatar;
            damegeTxt.text = characterSO.damege.ToString();
            healthTxt.text = characterSO.health.ToString();
        }

        public void ClaimBtn()
        {
            GameManager.Instance.AddCoin(5000);
            TurnOff();
        }

        public void ExitBtn()
        {
            TurnOff();
        }
    }
}