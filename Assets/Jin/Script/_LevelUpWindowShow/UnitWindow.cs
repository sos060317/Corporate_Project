using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net.Sockets;

public class UnitWindow : MonoBehaviour
{
    public GameUnitChake UnitClick;
    private RectTransform rectTransform;
    public bool buttonDown = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    private void FixedUpdate()
    {
        ShowTowerWindow();
        HideTowerWindow();
    }

    private void ShowTowerWindow()
    {
        if (UnitClick.clicked == true)
        {
            rectTransform.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);
        }
    }

    private void HideTowerWindow()
    {
        if (UnitClick.clicked == false)
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
