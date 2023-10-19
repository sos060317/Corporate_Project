using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TowerNodeBuildUI : MonoBehaviour
{
    public UpgradeArrowTower ATower;
    public BombTower BTower;
    public MagicPosCheck MTower;
    public Spawner UTower;

    public Image towerWindow;
    public RectTransform buttonImage;

    private RectTransform rectTransform;

    private bool isShowWindow = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ButtonShowFunction()
    {
        if (isShowWindow)
        {
            TowerBuildManager.Instance.HideTowerWindow();
        }
        else
        {
            ShowTowerWindow(null);
        }
    }

    public void ShowTowerWindow(TowerNode towerNode)
    {
        isShowWindow = true;
        buttonImage.rotation = Quaternion.Euler(0, 0, 180f);
        rectTransform.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);
    }

    public void HideTowerWindow()
    {
        isShowWindow = false;
        buttonImage.rotation = Quaternion.Euler(0, 0, 0f);
        rectTransform.DOAnchorPosY(-390, 0.3f).SetEase(Ease.Linear);
    }
}
