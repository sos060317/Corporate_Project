using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerNode : MonoBehaviour
{
    public Color overColor;                     // ���콺 ���� ����
    public Color nomalColor;                    // �⺻ ����

    private GameObject tower;                   // ��ġ �� �� �ִ� Ÿ��

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
