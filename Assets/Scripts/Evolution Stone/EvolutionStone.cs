using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EvolutionStone : MonoBehaviour
{
    [SerializeField] private GameObject[] upgradeButtons;
    
    private Vector2[] upgradeButtonsPos;

    private bool isShowUpgradeUI = false;

    private WaitForSeconds waitForSeconds;

    private void Start()
    {
        upgradeButtonsPos = new Vector2[upgradeButtons.Length];
        
        for(int i = 0; i < upgradeButtonsPos.Length; i++)
        {
            upgradeButtonsPos[i] = upgradeButtons[i].GetComponent<Transform>().position;
            upgradeButtons[i].gameObject.SetActive(false);
            upgradeButtons[i].gameObject.GetComponent<EvolutionStoneButton>().canClick = false;
        }

        waitForSeconds = new WaitForSeconds(0.3f);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (isShowUpgradeUI)
            {
                HideUpgradeUI();
            }
            
            Debug.Log("AA");
            
            return;
        }

        if (!isShowUpgradeUI)
        {
            ShowUpgradeUI();
        }
        else
        {
            HideUpgradeUI();
        }
    }
    
    private void ShowUpgradeUI()
    {
        isShowUpgradeUI = true;
        
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].GetComponent<Transform>().localPosition = Vector2.zero;
            upgradeButtons[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].GetComponent<Transform>().DOMove(upgradeButtonsPos[i], 0.3f);
        }

        StartCoroutine(ShowUIRoutine());
    }
    
    private void HideUpgradeUI()
    {
        isShowUpgradeUI = false;
        
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].GetComponent<Transform>().DOMove(transform.position, 0.3f);
            upgradeButtons[i].gameObject.GetComponent<EvolutionStoneButton>().canClick = false;
        }

        StartCoroutine(DisableUI());
    }
    
    private IEnumerator ShowUIRoutine()
    {
        yield return waitForSeconds;

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].gameObject.GetComponent<EvolutionStoneButton>().canClick = true;
        }
    }

    private IEnumerator DisableUI()
    {
        yield return waitForSeconds;
        
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}