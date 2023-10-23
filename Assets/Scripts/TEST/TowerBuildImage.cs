using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuildImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject ownTower;

    public void OnPointerClick(PointerEventData eventData)
    {
        TowerBuildManager.Instance.selectedNode.BuildTower(ownTower);
    }
}
