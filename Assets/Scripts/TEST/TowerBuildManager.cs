using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TowerBuildManager : MonoBehaviour
{
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
            DeselectNode();
            return;
        }

        if(selectedNode != null)
        {
            selectedNode.GetComponent<TowerNode>().isClick = false;
            selectedNode.GetComponent<SpriteRenderer>().color = selectedNode.GetComponent<TowerNode>().nomalColor;
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
