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

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ShowTowerWindow(TowerNode towerNode)
    {
        rectTransform.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);
    }

    public void HideTowerWindow()
    {
        rectTransform.DOAnchorPosY(-500, 0.3f).SetEase(Ease.Linear);
    }
}
