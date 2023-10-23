using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TowerBuildManager : MonoBehaviour
{
    public Color overColor;                     // 마우스 오버 색상
    public Color nomalColor;                    // 기본 색상
    public Color clickColor;                    // 클릭 색상

    public bool isClick;

    private static TowerBuildManager instance = null;

    public TowerNode selectedNode;

    public TowerNodeBuildUI towerNodeBuildUI;

    public static TowerBuildManager Instance
    {
        get 
        {
            if(null == instance)
            {
                return null;
            }

            return instance; 
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void ShowTowerWindow(TowerNode towerNode)
    {
        if(selectedNode == towerNode)
        {
            towerNode.GetComponent<SpriteRenderer>().color = nomalColor;
            DeselectNode();
            return;
        }

        selectedNode = towerNode;

        towerNodeBuildUI.ShowTowerWindow(towerNode);
    }

    public void DeselectNode()
    {
        HideTowerWindow();
    }

    public void HideTowerWindow()
    {
        selectedNode = null;
        towerNodeBuildUI.HideTowerWindow(); 
    }
}
