using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 게임 관련 변수

    [Space(10)]
    [Header("게임 관련 변수")]
    [SerializeField] private int startGoldCount;

    #endregion

    #region UI 관련 오브젝트

    [Space(10)]
    [Header("UI 관련 오브젝트")]
    [SerializeField] private TextMeshProUGUI goldText;

    public Transform buffIconParent;

    #endregion

    #region 버프 관련 변수들

    [HideInInspector] public float getGoldMultiply;
    [HideInInspector] public float allyAttackDamageMultiply;
    [HideInInspector] public float allyHealthMultiply;
    [HideInInspector] public float enemyAttackDamageMultiply;

    #endregion

    #region 진화석 관련

    [Space(10)] [Header("진화석 관련")]
    [SerializeField] private Image evolutionStoneHealthBar;

    [SerializeField] private float evolutionStoneMaxHealth;

    #endregion

    public float currentGold;

    private float evolutionStoneCurHealth;
    
    private static GameManager instance = null; // 해당 스크립트를 변수로 받아옴

    // 싱글톤 프로퍼티
    public static GameManager Instance
    {
        get
        {
            if (instance == null) // instance가 없으면
            {
                return null; // null이면 null return
            }
            return instance; // instance가 있으면 return
        }
    }

    private void Awake()
    {
        if (instance == null) // null이면
        {
            instance = this; // 넣어주고

            //DontDestroyOnLoad(this.gameObject); // 씬이 전환되어도 유지
        }
        else
        {
            Destroy(gameObject); // null이면 Destroy
        }
    }

    private void Start()
    {
        //변수 초기화
        currentGold = startGoldCount;
        evolutionStoneCurHealth = evolutionStoneMaxHealth;
        getGoldMultiply = 1;
        allyAttackDamageMultiply = 1;
        enemyAttackDamageMultiply = 1;
    }

    private void Update()
    {
        //goldText.text = currentGold.ToString();

        EvolutionHealthBarUpdate();
    }

    public void GetGold(float gold)
    {
        StartCoroutine(GoldCount(currentGold + (gold * getGoldMultiply), currentGold));

        currentGold += gold * getGoldMultiply;
    }

    private IEnumerator GoldCount(float target, float current)
    {
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * Time.deltaTime;
            goldText.text = ((int)current).ToString();
            yield return null;
        }

        current = target;
        goldText.text = Mathf.FloorToInt(current).ToString();
    }

    public void UseGold(float gold)
    {
        StartCoroutine(GoldCountDown(currentGold - gold, currentGold));

        currentGold -= gold;
    }
    
    private IEnumerator GoldCountDown(float target, float current)
    {
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 
        float offset = (current - target) / duration;

        while (current > target)
        {
            current -= offset * Time.deltaTime;
            goldText.text = ((int)current).ToString();
            yield return null;
        }

        current = target;
        goldText.text = Mathf.FloorToInt(current).ToString();
    }

    public void OnEvolutionStoneDamaged(float attackPower, float spellPower)
    {
        evolutionStoneCurHealth =
            Mathf.Max(
                evolutionStoneCurHealth - (attackPower + spellPower), 0);

        if (evolutionStoneCurHealth <= 0)
        {
            // 게임 오버 로직.
        }
    }
    
    private void EvolutionHealthBarUpdate()
    {
        evolutionStoneHealthBar.fillAmount = Mathf.Lerp(evolutionStoneHealthBar.fillAmount,
            evolutionStoneCurHealth / evolutionStoneMaxHealth, Time.deltaTime * 12);
    }
}
