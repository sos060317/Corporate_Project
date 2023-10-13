using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EasyTransition;

public class GameClearMenu : MonoBehaviour
{
    [SerializeField] private RectTransform[] starRects;
    [SerializeField] private GameObject[] particleObjs;
    [SerializeField] private TransitionSettings transitionSettings;

    public void Init(int starCount)
    {
        for (int i = 0; i < starRects.Length; i++)
        {
            starRects[i].localScale = Vector3.zero;
            starRects[i].gameObject.SetActive(false);
        }

        StartCoroutine(EnableRoutine(starCount));
    }
    
    private IEnumerator EnableRoutine(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            starRects[i].gameObject.SetActive(true);
            starRects[i].DOScale(new Vector3(1, 1, 1), 0.4f).SetEase(Ease.OutBack);
            particleObjs[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void LoadScene(string sceneName)
    {
        TransitionManager.Instance().Transition(sceneName, transitionSettings, 0);
    }
}