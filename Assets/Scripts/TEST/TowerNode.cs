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
        TowerBuildManager.Instance.ShowTowerWindow(this);
    }
}
