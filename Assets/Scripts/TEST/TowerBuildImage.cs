using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuildImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject ownTower;
    [SerializeField] private float towerInstallGold;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.currentGold < towerInstallGold)
        {
            Debug.Log("골드 부족");
            
            return;
        }

        TowerBuildManager.Instance.selectedNode.GetComponent<TowerNode>().isClick = false;
        TowerBuildManager.Instance.selectedNode.GetComponent<SpriteRenderer>().color = TowerBuildManager.Instance.selectedNode.GetComponent<TowerNode>().nomalColor;

        TowerBuildManager.Instance.towerNodeBuildUI.BuildTower(ownTower);

        TowerBuildManager.Instance.DeselectNode();
    }
}
