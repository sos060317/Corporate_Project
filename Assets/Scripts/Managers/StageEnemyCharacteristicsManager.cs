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

    public float health;          //ü��
    public float moveSpeed;       //�̵� �ӵ�
    public float attackRate;      //������ ���ϴ� �ӵ�
    public float attackRange;     //������ ���ϴ� ����
    public float defnse;          //���� ����
    public float magicResistance; //���� ����
    public float attackPower;     //���ݷ�
    public float spellPower;      //������
    public float coin;            //���� ����

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
        // ��� ���� ���� ���� �´� ������ �ʱ�ȭ
        health = ch.enemyBaseHealth;
        moveSpeed = ch.enemyBaseMoveSpeed;
        attackRate = ch.enemyBaseAttackRate;
        attackRange = ch.enemyBaseAttackRange;
        defnse = ch.defense;
        magicResistance = ch.magicResistance;
        attackPower = ch.attackPower;
        spellPower = ch.spellPower;
        coin = ch.coins;

        // �г� �ʱ�ȭ
        chEnemyImage.sprite = ch.profileImage;
        chEnemyName.text = ch.enemyName + " Characteristics";
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
