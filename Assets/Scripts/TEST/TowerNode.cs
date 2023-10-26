
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerNode : MonoBehaviour
{
    public Color overColor;                     // 마우스 오버 색상
    public Color nomalColor;                    // 기본 색상
    public Color clickColor;                    // 클릭 색상

    public bool isClick;

    public GameObject tower;                   // 설치 할 수 있는 타워

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

        isClick = true;
        spriteRenderer.color = clickColor;

        if(TowerBuildManager.Instance.selectedNode == this)
        {
            isClick = false;
            spriteRenderer.color = overColor;
        }

        TowerBuildManager.Instance.ShowTowerWindow(this);
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
        if (spriteRenderer.color.a != 0 && !isClick)
        {
            spriteRenderer.color = overColor;
        }
    }

    private void OnMouseExit()
    {
        if (spriteRenderer.color.a != 0 && !isClick)
        {
            spriteRenderer.color = nomalColor;
        }
    }
}
