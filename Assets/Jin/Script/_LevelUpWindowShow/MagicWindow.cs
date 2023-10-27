using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net.Sockets;

public class MagicWindow : MonoBehaviour
{
    public MagicPosCheck MagicClick;
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
        if (MagicClick.isclick == true)
        {
            rectTransform.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);
        }
    }

    private void HideTowerWindow()
    {
        if (MagicClick.isclick == false)
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
