using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class SettingUIHandler : MonoBehaviour
{
    [Header("Canvas Group")]
    [SerializeField] CanvasGroup canvasGroup;
    [Header("Animation Variables")]
    [SerializeField] float fadeInDur;
    [SerializeField] float fadeOutDur;

    [SerializeField] private RectTransform rectTransform;

    public bool isOpen = false;
    void Start()
    {

    }
    public void TurnOn()
    {
        canvasGroup.alpha = 0;
        rectTransform.gameObject.SetActive(true);
        canvasGroup.DOFade(1, fadeInDur);
        rectTransform.DOScale(new Vector3(1, 1, 1), fadeInDur);
    }
    public void TurnOff()
    {
        rectTransform.DOScale(new Vector3(0.1f, 1, 1), fadeOutDur);
        canvasGroup.DOFade(0, fadeOutDur).onComplete += () =>
        {
            rectTransform.gameObject.SetActive(false);
        };
    }
}
