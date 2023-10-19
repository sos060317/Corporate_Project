using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerNode : MonoBehaviour
{
    public Color overColor;                     // 마우스 오버 색상
    public Color nomalColor;                    // 기본 색상

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
    }

    private void OnMouseEnter()
    {
        if (spriteRenderer.color.a != 0)
        {
            spriteRenderer.color = overColor;
        }
    }

    private void OnMouseExit()
    {
        if (spriteRenderer.color.a != 0)
        {
            spriteRenderer.color = nomalColor;
        }
    }
}
