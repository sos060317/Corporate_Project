using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private static BuildManager instance = null;

    public Node selectNode;

    public NodeBuildUI nodeBuildUI;

    public NodeUpgradeUI nodeUpgradeUI;

    public static BuildManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowBuildWindow(Node node)
    {
        if (selectNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectNode = node;
        
        nodeBuildUI.ShowBuildUI(node);
    }

    public void ShowUpgradeWindow(Node node)
    {
        if (selectNode == node)
        {
            DeselectNode();
            return;
        }

        selectNode = node;
        
        nodeUpgradeUI.ShowUpgradeUI(node);
    }
    
    public void DeselectNode()
    {
        HideBuildUI();
        HideUpgradeUI();
    }
    
    public void HideBuildUI()
    {
        selectNode = null;
        nodeBuildUI.HideBuildUI();
    }
    
    public void HideUpgradeUI()
    {
        selectNode = null;
        nodeUpgradeUI.HideUpgradeUI();
    }
}
