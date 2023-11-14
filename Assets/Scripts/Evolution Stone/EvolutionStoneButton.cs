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
    //[SerializeField] private Image infoBg;
    [SerializeField] protected Text goldText;
    
    
    #endregion

    #region 게임 오브젝트 관련

    [Space(10)] [Header("게임 오브젝트 관련")]
    [SerializeField] protected GameObject levelMaxText;
    [SerializeField] protected Transform levelStarParent;
    [SerializeField] protected GameObject levelStarPrefab;
    [SerializeField] protected BuffIcon iconPrefab;
    
    #endregion

    [HideInInspector] public bool canClick;
    
    protected int curLevel = 0;

    private float fadeAmount;

    protected BuffIcon icon;

    public void Init()
    {
        icon = Instantiate(iconPrefab, GameManager.Instance.buffIconParent);
        icon.TextChange(curLevel.ToString());
    
        goldText.text = buffDetails.buffDatas[curLevel].needGold.ToString();
    }
    
    public abstract void LevelUp();
}