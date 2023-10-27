using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextWaveInfo : MonoBehaviour
{
    [SerializeField] private Text infoText;
    [SerializeField] private Image infoBg;

    [SerializeField] private GameObject indicatorObj;
    [SerializeField] private LayerMask indicatorLayer;

    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Camera camera;

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

    private void Update()
    {
        IndicatorUpdate();
    }

    public void InitInfo(List<string> enemyNameList, List<int> enemyCountList)
    {   
        infoText.text = "정보\n\n";

        for (int i = 0; i < enemyNameList.Count; i++)
        {
            infoText.text += enemyNameList[i] + " X " + enemyCountList[i];
            
            if (enemyNameList.Count - i - 1 > 0)
            {
                infoText.text += "\n";
            }
        }
    }

    private void IndicatorUpdate()
    {
        // 화면 밖으로 나가면 카메라 쪽으로 이동
        if (renderer.isVisible == false)
        {
            if (indicatorObj.activeSelf == false)
            {
                indicatorObj.SetActive(true);
            }
            
            Vector2 direction = GameManager.Instance.cameraObj.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, indicatorLayer);
            
            if (ray.collider != null)
            {
                Vector2 pos = camera.WorldToScreenPoint(ray.point);
                
                indicatorObj.transform.position = pos;
            }
        }
        else
        {
            if (indicatorObj.activeSelf)
            {
                indicatorObj.SetActive(false);
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
    
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        WaveStart();
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
    
    public void WaveStart()
    {
        WaveManager.Instance.WaveStart();
    }
}