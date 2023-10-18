using System.Collections;
using System.Collections.Generic;
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
        selectedNode = towerNode;

        towerNodeBuildUI.ShowTowerWindow(towerNode);
    }
}
