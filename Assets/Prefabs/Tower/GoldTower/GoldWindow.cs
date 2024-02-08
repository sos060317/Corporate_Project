using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net.Sockets;

public class GoldWindow : MonoBehaviour
{
    public GoldTower goldClick;
    private RectTransform rectTransform;
    public bool buttonDown = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    private void Update()
    {
        ShowTowerWindow();
        HideTowerWindow();
    }

    private void ShowTowerWindow()
    {
        if (goldClick.UIClick == false)
        {
            rectTransform.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);
        }
    }

    private void HideTowerWindow()
    {
        if (goldClick.UIClick == true)
        {
            rectTransform.DOAnchorPosY(-360, 0.3f).SetEase(Ease.Linear);
        }
    }

    public void HideTowerWindowButton()
    {
        buttonDown = true;
        rectTransform.DOAnchorPosY(-360, 0.3f).SetEase(Ease.Linear);
    }
}
