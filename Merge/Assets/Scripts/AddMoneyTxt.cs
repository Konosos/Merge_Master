using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace MergeHero
{
    public class AddMoneyTxt : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyTxt;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Start()
        {
            
        }

        public void SetUpAndFly(Vector3 pos, int numCoin)
        {

            moneyTxt.text = "+" + numCoin.ToString();

            canvasGroup.alpha = 1;
            RectTransform txtRect = moneyTxt.rectTransform;
            txtRect.DOScale(Vector3.one, 1.2f).SetEase(Ease.OutSine);
            transform.position = pos;
            transform.DOMoveY(pos.y + 3, 1.2f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                canvasGroup.DOFade(0, 0.5f).SetEase(Ease.OutQuint);
                transform.DOMoveY(transform.position.y + 2, 0.5f).SetEase(Ease.OutQuint).OnComplete(() =>
                {
                    
                    this.gameObject.SetActive(false);
                });

            });
        }
    }
}