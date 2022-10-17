using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace MergeHero
{
    public class AddMoneyTxt : MonoBehaviour
    {
        public TMP_Text moneyTxt;

        public void SetUpAndFly(Vector3 pos, int numCoin)
        {

            moneyTxt.text = "+" + numCoin.ToString();
            transform.position = pos;
            transform.DOMoveY(pos.y + 5, 1.5f).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        }
    }
}