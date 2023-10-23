using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerNode : MonoBehaviour
{
    private GameObject tower;                   // 설치 할 수 있는 타워

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(tower != null)
        {
            TowerBuildManager.Instance.HideTowerWindow();

            return;
        }
        TowerBuildManager.Instance.ShowTowerWindow(this);
        spriteRenderer.color = TowerBuildManager.Instance.clickColor;
        TowerBuildManager.Instance.isClick = true;
    }

    public void BuildTower(GameObject towerPrefab)
    {
        tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);

        var srColor = spriteRenderer.color;
        srColor.a = 0;
        spriteRenderer.color = srColor;

    }

    private void OnMouseEnter()
    {
        if (spriteRenderer.color.a != 0)
        {
            spriteRenderer.color = TowerBuildManager.Instance.overColor;
        }
    }

    private void OnMouseExit()
    {
        if (spriteRenderer.color.a != 0 && !TowerBuildManager.Instance.isClick)
        {
            spriteRenderer.color = TowerBuildManager.Instance.nomalColor;
        }
    }
}
