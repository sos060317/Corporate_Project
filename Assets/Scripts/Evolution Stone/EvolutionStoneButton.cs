using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class EvolutionStoneButton : MonoBehaviour
{
    [SerializeField] protected BuffDetailsSO buffDetails;

    #region UI 관련

    [Space(10)] [Header("UI 관련")]
    [SerializeField] protected Text infoText;
    [SerializeField] private Image infoBg;
    [SerializeField] protected Text goldText;
    
    
    #endregion

    #region 게임 오브젝트 관련

    [Space(10)] [Header("게임 오브젝트 관련")]
    [SerializeField] protected GameObject levelMaxText;
    [SerializeField] protected Transform levelStarParent;
    [SerializeField] protected ParticleSystem levelUpEffect;
    [SerializeField] protected GameObject levelStarPrefab;
    [SerializeField] protected BuffIcon iconPrefab;
    
    #endregion

    protected int curLevel = 0;

    private float fadeAmount;

    protected BuffIcon icon;

    private void Start()
    {
        Color infoTextColor = infoText.color;
        Color infoBgColor = infoBg.color;

        infoTextColor.a = 0f;
        infoBgColor.a = 0f;
        
        infoText.color = infoTextColor;
        infoBg.color = infoBgColor;

        goldText.text = buffDetails.buffDatas[curLevel].needGold.ToString();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (curLevel >= buffDetails.buffDatas.Length)
        {
            return;
        }

        if (GameManager.Instance.currentGold >= buffDetails.buffDatas[curLevel].needGold)
        {
            LevelUp();
        }
    }
    
    private void OnMouseEnter()
    {
        StopAllCoroutines();
        StartCoroutine(ShowInfo());
    }
    
    private void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(HideInfo());
    }
    
    private IEnumerator ShowInfo()
    {
        Color infoTextColor = infoText.color;
        Color infoBgColor = infoBg.color;

        float textAlpha = 1f;
        float bgAlpha = 0.4f;

        while (fadeAmount <= 1)
        {
            infoTextColor.a = fadeAmount * textAlpha;
            infoBgColor.a = fadeAmount * bgAlpha;

            infoText.color = infoTextColor;
            infoBg.color = infoBgColor;

            fadeAmount += Time.deltaTime * 5;

            yield return null;
        }

        fadeAmount = 1f;
        
        infoTextColor.a = fadeAmount * textAlpha;
        infoBgColor.a = fadeAmount * bgAlpha;

        infoText.color = infoTextColor;
        infoBg.color = infoBgColor;
    }
    
    private IEnumerator HideInfo()
    {
        Color infoTextColor = infoText.color;
        Color infoBgColor = infoBg.color;

        float textAlpha = 1f;
        float bgAlpha = 0.4f;

        while (fadeAmount >= 0)
        {
            infoTextColor.a = fadeAmount * textAlpha;
            infoBgColor.a = fadeAmount * bgAlpha;

            infoText.color = infoTextColor;
            infoBg.color = infoBgColor;

            fadeAmount -= Time.deltaTime * 5;

            yield return null;
        }

        fadeAmount = 0f;
        
        infoTextColor.a = fadeAmount * textAlpha;
        infoBgColor.a = fadeAmount * bgAlpha;

        infoText.color = infoTextColor;
        infoBg.color = infoBgColor;
    }

    protected abstract void LevelUp();
}