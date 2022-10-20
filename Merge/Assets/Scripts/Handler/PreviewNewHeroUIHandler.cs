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
        [SerializeField] private GameObject bg;
        private GameObject newHero;

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
                Destroy(newHero);
            };
        }

        private IEnumerator OnTurnOn(string charName)
        {
            yield return null;
            CharacterSO characterSO = System.Array.Find(ChessCreater.Instance.characterSOs, character => character.nameChar == charName);
            avatar.sprite = characterSO.avatar;


            newHero = Instantiate(characterSO.prefab, Vector3.zero, Quaternion.identity);
            newHero.GetComponent<Animator>().SetTrigger("Victory");
            Transform charTrans = newHero.transform;           
            charTrans.SetParent(bg.transform);
            charTrans.localPosition = new Vector3(0, -250, -200);
            charTrans.localEulerAngles = new Vector3(0, 180, 0);
            charTrans.localScale = Vector3.one * 180f;
            charTrans.DOLocalRotate(new Vector3(0, -180, 0), 6, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

            damegeTxt.text = characterSO.damege.ToString();
            healthTxt.text = characterSO.health.ToString();
        }

        public void ClaimBtn()
        {
            GameManager.Instance.AddCoin(5000);
            TurnOff();

            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }

        public void ExitBtn()
        {
            TurnOff();

            SoundManager.Instance.PlaySFXByPublicSource(GameConfigs.CLICK_KEY, 0.7f);
        }
    }
}