using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimUI : MonoBehaviour
{
    [SerializeField]
    Ease ease;

    public void ShowUI()
    {
        transform.DOKill();
        GetComponent<CanvasGroup>().DOFade(1,0.75f).SetEase(ease);
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void HideUI()
    {
        transform.DOKill();
        GetComponent<CanvasGroup>().DOFade(0, 0.55f).SetEase(ease);
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}