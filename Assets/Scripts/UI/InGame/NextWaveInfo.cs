using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveInfo : MonoBehaviour
{
    [SerializeField] private Text infoText;
    [SerializeField] private Image infoBg;

    private float fadeAmount = 0f;

    private void Start()
    {
        Color infoTextColor = infoText.color;
        Color infoBgColor = infoBg.color;

        infoTextColor.a = 0f;
        infoBgColor.a = 0f;
        
        infoText.color = infoTextColor;
        infoBg.color = infoBgColor;
    }

    public void InitInfo(List<string> enemyNameList, List<int> enemyCountList)
    {   
        infoText.text = "정보\n\n";

        for (int i = 0; i < enemyNameList.Count; i++)
        {
            infoText.text += enemyNameList[i] + " X " + enemyCountList[i].ToString();
            
            if (i - enemyNameList.Count > 0)
            {
                infoText.text += "\n";
            }
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

            fadeAmount += Time.deltaTime * 4;

            yield return null;
        }

        fadeAmount = 1f;
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

            fadeAmount -= Time.deltaTime * 4;

            yield return null;
        }

        fadeAmount = 0f;
    }

    private void OnDisable()
    {
        Color infoTextColor = infoText.color;
        Color infoBgColor = infoBg.color;

        infoTextColor.a = 0f;
        infoBgColor.a = 0f;
        
        infoText.color = infoTextColor;
        infoBg.color = infoBgColor;
        
        fadeAmount = 0f;
    }
}