using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NodeUpgradeUI : MonoBehaviour
{
    public Button[] buildButtons;
    
    private Vector2[] buildButtonsPos;

    private Node selectNode;
    
    private WaitForSeconds waitForSeconds;
    
    private void Start()
    {
        buildButtonsPos = new Vector2[buildButtons.Length];
        
        for(int i = 0; i < buildButtonsPos.Length; i++)
        {
            buildButtonsPos[i] = buildButtons[i].GetComponent<RectTransform>().anchoredPosition;
            buildButtons[i].gameObject.SetActive(false);
        }

        waitForSeconds = new WaitForSeconds(0.3f);
    }
    
    public void ShowUpgradeUI(Node node)
    {
        transform.position = node.transform.position;

        selectNode = node;
        
        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            buildButtons[i].interactable = false;
            buildButtons[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].GetComponent<RectTransform>().DOAnchorPos(buildButtonsPos[i], 0.3f);
        }

        StartCoroutine(ButtonRoutine());
    }
    
    public void HideUpgradeUI()
    {
        selectNode = null;
        
        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].interactable = false;
            buildButtons[i].GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.3f);
        }

        StartCoroutine(DisableButton());
    }
    
    public void DestroyTower()
    {
        selectNode.DestoryTower();
        
        BuildManager.Instance.DeselectNode();
    }
    
    IEnumerator ButtonRoutine()
    {
        yield return waitForSeconds;

        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].interactable = true;
        }
    }
    
    IEnumerator DisableButton()
    {
        yield return waitForSeconds;
        
        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].gameObject.SetActive(false);
        }
    }
}
