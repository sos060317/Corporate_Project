using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageEnemyCharacteristicsManager : MonoBehaviour
{
    [SerializeField] private StageEnemyCharacteristicsSO ch;

    [SerializeField] private Image chEnemyImage;
    [SerializeField] private TextMeshProUGUI chEnemyName;
    [SerializeField] private TextMeshProUGUI chEnemyEffect;

    private RectTransform rect;

    [HideInInspector] public float health;          //체력
    [HideInInspector] public float moveSpeed;       //이동 속도
    [HideInInspector] public float attackRate;      //공격을 가하는 속도
    [HideInInspector] public float attackRange;     //공격을 가하는 범위
    [HideInInspector] public float defnse;          //물리 방어력
    [HideInInspector] public float magicResistance; //마법 방어력
    [HideInInspector] public float attackPower;     //공격력
    [HideInInspector] public float spellPower;      //마법력
    [HideInInspector] public float coin;            //코인 감소

    private static StageEnemyCharacteristicsManager instance = null;

    public static StageEnemyCharacteristicsManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 모든 변수 현제 씬에 맞는 버프로 초기화
        health = ch.enemyBaseHealth;
        moveSpeed = ch.enemyBaseMoveSpeed;
        attackRate = ch.enemyBaseAttackRate;
        attackRange = ch.enemyBaseAttackRange;
        defnse = ch.defense;
        magicResistance = ch.magicResistance;
        attackPower = ch.attackPower;
        spellPower = ch.spellPower;
        coin = ch.coins;

        // 패널 초기화
        chEnemyImage.sprite = ch.profileImage;
        chEnemyName.text = ch.enemyName + " 특성";
        chEnemyEffect.text = ch.changeStat;

        //StartCoroutine(ShowChWindow());

        rect = GetComponent<RectTransform>();

        rect.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutBack);
        rect.DOAnchorPosX(1700, 0.5f).SetDelay(3f).SetEase(Ease.InBack);
    }

    IEnumerator ShowChWindow()
    {
        yield return null;
    }
}
