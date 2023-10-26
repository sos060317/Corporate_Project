using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageEnemyCharacteristicsManager : MonoBehaviour
{
    [SerializeField] private StageEnemyCharacteristicsSO ch;

    [SerializeField] private Image chEnemyImage;
    [SerializeField] private TextMeshProUGUI chEnemyName;
    [SerializeField] private TextMeshProUGUI chEnemyEffect;

    float health;          //체력
    float moveSpeed;       //이동 속도
    float attackRate;      //공격을 가하는 속도
    float attackRange;     //공격을 가하는 범위
    float defnse;          //물리 방어력
    float magicResistance; //마법 방어력
    float attackPower;     //공격력
    float spellPower;      //마법력
    float coin;            //코인 감소

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
        health = ch.enemyBaseHealth;
        moveSpeed = ch.enemyBaseMoveSpeed;
        attackRate = ch.enemyBaseAttackRate;
        attackRange = ch.enemyBaseAttackRange;
        defnse = ch.defense;
        magicResistance = ch.magicResistance;
        attackPower = ch.attackPower;
        spellPower = ch.spellPower;
        coin = ch.coins;

        chEnemyImage.sprite = ch.profileImage;
        chEnemyName.text = ch.enemyName + " Characteristics";
        chEnemyEffect.text = ch.changeStat;
    }
}
