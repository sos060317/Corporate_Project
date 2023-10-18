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

    private GameManager playerCoin;

    public Image towerWindow;

    private Vector2 towerWindowPos;

    private WaitForSeconds waitForSeconds;

    private TowerNode selectedNode;

    private RectTransform rectTransform;

    private void Start()
    {
        towerWindowPos = towerWindow.GetComponent<RectTransform>().anchoredPosition;
        rectTransform = GetComponent<RectTransform>();
        playerCoin = GameManager.Instance;
        waitForSeconds = new WaitForSeconds(0.3f);
    }

    public void ShowTowerWindow(TowerNode towerNode)
    {
        selectedNode = towerNode;

        //towerWindow.GetComponent<RectTransform>().DOAnchorPos(towerWindowPos, 0.3f);
        rectTransform.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);
    }
}
