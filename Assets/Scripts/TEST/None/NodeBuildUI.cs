using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NodeBuildUI : MonoBehaviour
{
    //private UpgradeArrowTower UpArrowTower;
    //private GameManager playerCoin;

    //public Button[] buildButtons;

    //private Vector2[] buildButtonsPos;

    //private WaitForSeconds waitForSeconds;

    //private Node selectNode;

    //private void Start()
    //{
    //    buildButtonsPos = new Vector2[buildButtons.Length];

    //    for(int i = 0; i < buildButtonsPos.Length; i++)
    //    {
    //        buildButtonsPos[i] = buildButtons[i].GetComponent<RectTransform>().anchoredPosition;
    //        buildButtons[i].gameObject.SetActive(false);
    //    }

    //    waitForSeconds = new WaitForSeconds(0.3f);
    //}

    //public void ShowBuildUI(Node node)
    //{
    //    transform.position = node.transform.position;

    //    selectNode = node;

    //    for (int i = 0; i < buildButtons.Length; i++)
    //    {
    //        buildButtons[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    //        buildButtons[i].gameObject.SetActive(true);
    //        buildButtons[i].interactable = false;
    //    }

    //    for (int i = 0; i < buildButtons.Length; i++)
    //    {
    //        buildButtons[i].GetComponent<RectTransform>().DOAnchorPos(buildButtonsPos[i], 0.3f);
    //    }

    //    StartCoroutine(ButtonRoutine());
    //}

    //public void HideBuildUI()
    //{
    //    selectNode = null;

    //    for (int i = 0; i < buildButtons.Length; i++)
    //    {
    //        buildButtons[i].interactable = false;
    //        buildButtons[i].GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.3f);
    //    }

    //    StartCoroutine(DisableButton());
    //}

    //public void BuildTower(GameObject towerPrefab)  // 타워 설치? 아마
    //{
    //    selectNode.BuildTower(towerPrefab);

    //    BuildManager.Instance.DeselectNode();
    //}

    //IEnumerator ButtonRoutine()
    //{
    //    yield return waitForSeconds;

    //    for (int i = 0; i < buildButtons.Length; i++)
    //    {
    //        buildButtons[i].interactable = true;
    //    }
    //}

    //IEnumerator DisableButton()
    //{
    //    yield return waitForSeconds;

    //    for (int i = 0; i < buildButtons.Length; i++)
    //    {
    //        buildButtons[i].gameObject.SetActive(false);
    //    }
    //}


    //세이브용

    public UpgradeArrowTower ATower;
    public BombTower BTower;
    public MagicPosCheck MTower;
    public Spawner UTower;

    private GameManager playerCoin;

    public Button[] buildButtons;

    private Vector2[] buildButtonsPos;

    private WaitForSeconds waitForSeconds;

    private Node selectNode;



    private void Start()
    {
        buildButtonsPos = new Vector2[buildButtons.Length];

        for (int i = 0; i < buildButtonsPos.Length; i++)
        {
            buildButtonsPos[i] = buildButtons[i].GetComponent<RectTransform>().anchoredPosition;
            buildButtons[i].gameObject.SetActive(false);
        }
        playerCoin = GameManager.Instance;
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

    public void BuildTower(GameObject towerPrefab)  // 타워 설치? 아마
    {
        Debug.Log($"{playerCoin == null} {ATower == null}");
        if (playerCoin.currentGold >= ATower.StartingCost || playerCoin.currentGold >= BTower.Startcoin || playerCoin.currentGold >= MTower.Startingcoin || playerCoin.currentGold >= UTower.StartingCoin)
        {
            selectNode.BuildTower(towerPrefab);

            BuildManager.Instance.DeselectNode();
        }
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
