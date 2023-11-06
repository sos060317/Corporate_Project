using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyStatusWindow : MonoBehaviour
{
    [SerializeField] private Image profileImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Text healthText;
    [SerializeField] private Text healthStatText;
    [SerializeField] private Text attackStatText;
    [SerializeField] private Text spellStatText;
    [SerializeField] private Text speedStatText;
    [SerializeField] private Text defenseStatText;
    [SerializeField] private Text magicResistanceStatText;

    public bool isShowWindow;
    private bool isMovingWindow;

    private WaitForSeconds showDelay;
    private RectTransform rt;
    private EnemyBase enemyStat;
    private AllyBase allyStat;

    private void Start()
    {
        isShowWindow = false;
        
        showDelay = new WaitForSeconds(0.3f);
        rt = GetComponent<RectTransform>();
    }

    public void ShowWindow(EnemyBase enemyBase)
    {
        if (isMovingWindow)
        {
            return;
        }
        
        if (allyStat != null)
        {
            allyStat = null;
        }

        enemyStat = enemyBase;

        StartCoroutine(ShowRoutineEnemy());
        
        isShowWindow = true;
    }
    
    public void ShowWindow(AllyBase allyBase)
    {
        if (isMovingWindow)
        {
            return;
        }
        
        if (enemyStat != null)
        {
            enemyStat = null;
        }

        allyStat = allyBase;
        
        StartCoroutine(ShowRoutineAlly());
        
        isShowWindow = true;
    }
    
    private IEnumerator ShowRoutineEnemy()
    {
        isMovingWindow = true;
        
        if (isShowWindow)
        {
            rt.DOAnchorPosY(-500, 0.3f).SetEase(Ease.Linear);
            
            yield return showDelay;
        }
        
        profileImage.sprite = enemyStat.enemyDetailsSo.profileImage;
        nameText.text = enemyStat.enemyDetailsSo.enemyName;
        healthStatText.text = "체력 : " + enemyStat.maxHealth;
        attackStatText.text = "공격력 : " + enemyStat.attackPower;
        spellStatText.text = "마법공격력 : " + enemyStat.spellPower;
        speedStatText.text = "스피드 : " + enemyStat.moveSpeed;
        defenseStatText.text = "방어력 : " + enemyStat.defense + "%";
        magicResistanceStatText.text = "마법저항력 : " + enemyStat.magicResistance + "%";
        
        profileImage.SetNativeSize();
        
        rt.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);

        yield return showDelay;

        isMovingWindow = false;
    }
    
    private IEnumerator ShowRoutineAlly()
    {
        isMovingWindow = true;
        
        if (isShowWindow)
        {
            rt.DOAnchorPosY(-500, 0.3f).SetEase(Ease.Linear);

            yield return showDelay;
        }
        
        profileImage.sprite = allyStat.allyDetailsSo.profileImage;
        nameText.text = allyStat.allyDetailsSo.allyName;
        healthStatText.text = "체력 : " + allyStat.maxHealth;
        attackStatText.text = "공격력 : " + allyStat.allyDetailsSo.attackPower * GameManager.Instance.allyAttackDamageMultiply;
        spellStatText.text = "마법공격력 : " + allyStat.allyDetailsSo.spellPower * GameManager.Instance.allyAttackDamageMultiply;
        speedStatText.text = "스피드 : " + allyStat.allyDetailsSo.allyBaseMoveSpeed;
        defenseStatText.text = "방어력 : " + allyStat.allyDetailsSo.defense + "%";
        magicResistanceStatText.text = "마법저항력 : " + allyStat.allyDetailsSo.magicResistance + "%";
        
        profileImage.SetNativeSize();
        
        rt.DOAnchorPosY(0, 0.3f).SetEase(Ease.Linear);

        yield return showDelay;

        isMovingWindow = false;
    }

    private void Update()
    {
        if (enemyStat != null)
        {
            healthText.text = enemyStat.curHealth + " / " + enemyStat.enemyDetailsSo.enemyBaseHealth;
            healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount,
                enemyStat.curHealth / enemyStat.enemyDetailsSo.enemyBaseHealth, Time.deltaTime * 12);
        }
        else if (allyStat != null)
        {
            healthText.text = allyStat.curHealth + " / " + allyStat.maxHealth;
            healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount,
                allyStat.curHealth / allyStat.allyDetailsSo.allyBaseHealth, Time.deltaTime * 12);
        }
    }
    
    public void CloseWindow()
    {
        if (!isShowWindow)
        {
            return;
        }

        isShowWindow = false;
        
        allyStat = null;
        enemyStat = null;

        StartCoroutine(CloseRoutine());
    }
    
    private IEnumerator CloseRoutine()
    {
        isMovingWindow = true;

        rt.DOAnchorPosY(-500, 0.3f).SetEase(Ease.Linear);

        yield return showDelay;

        isMovingWindow = false;
    }
}