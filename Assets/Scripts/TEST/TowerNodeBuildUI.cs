using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net.Sockets;

public class TowerNodeBuildUI : MonoBehaviour
{
    public UpgradeArrowTower ATower;
    public BombTower BTower;
    public MagicPosCheck MTower;
    public Spawner UTower;

    private GameManager playerCoin;

    public Image towerWindow;

    public RectTransform buttonImage;

    private RectTransform rectTransform;

    private TowerNode selectedTowerNode;

    private bool isShowWindow = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ButtonShowFunction()
    {
        if (isShowWindow)
        {
            if(TowerBuildManager.Instance.selectedNode != null)
            {
                TowerBuildManager.Instance.selectedNode.GetComponent<TowerNode>().isClick = false;
                TowerBuildManager.Instance.selectedNode.GetComponent<SpriteRenderer>().color = TowerBuildManager.Instance.selectedNode.GetComponent<TowerNode>().nomalColor;
            }
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
        rectTransform.DOAnchorPosY(-270, 0.3f).SetEase(Ease.Linear);
    }

    public void BuildTower(GameObject towerPrefab)  // 타워 설치? 아마
    {
        if (playerCoin.currentGold >= ATower.StartingCost || playerCoin.currentGold >= BTower.Startcoin || playerCoin.currentGold >= MTower.Startingcoin || playerCoin.currentGold >= UTower.StartingCoin)
        {
            //selectedNode.BuildTower(towerPrefab);
            selectedTowerNode.BuildTower(towerPrefab);

            BuildManager.Instance.DeselectNode();
        }
    }
}
