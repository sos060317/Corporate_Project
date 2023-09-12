using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NodeBuildUI : MonoBehaviour
{
    public Button[] buildButtons;
    
    private Vector2[] buildButtonsPos;

    private WaitForSeconds waitForSeconds;

    private Node selectNode;

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

    public void ShowBuildUI(Node node)
    {
        transform.position = node.transform.position;

        selectNode = node;
        
        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            buildButtons[i].gameObject.SetActive(true);
            buildButtons[i].interactable = false;
        }

        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].GetComponent<RectTransform>().DOAnchorPos(buildButtonsPos[i], 0.3f);
        }

        StartCoroutine(ButtonRoutine());
    }
    
    public void HideBuildUI()
    {
        selectNode = null;
        
        for (int i = 0; i < buildButtons.Length; i++)
        {
            buildButtons[i].interactable = false;
            buildButtons[i].GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.3f);
        }

        StartCoroutine(DisableButton());
    }

    public void BuildTower(GameObject towerPrefab)
    {

        selectNode.BuildTower(towerPrefab);
        
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
