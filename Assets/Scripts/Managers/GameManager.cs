using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    
    [SerializeField] private Text waveText;

    [SerializeField] private GameClearMenu gameClearMenu;

    [SerializeField] private GameOverMenu gameOverMenu;

    [SerializeField] private GamePauseMenu gamePauseMenu;

    [SerializeField] private GameObject evolutionUpgradeMenu;

    public Transform buffIconParent;

    #endregion

    #region 버프 관련 변수들

    [HideInInspector] public float getGoldMultiply = 1;
    [HideInInspector] public float towerAttackDamageMultiply = 1;
    [HideInInspector] public float skillAttackDamageMultiply = 1;
    [HideInInspector] public float skillCoolTimeMultiply = 1;

    #endregion

    #region 진화석 관련

    [Space(10)] [Header("진화석 관련")]
    [SerializeField] private Image evolutionStoneHealthBar;

    [SerializeField] private float evolutionStoneMaxHealth;

    [SerializeField] private int levelIndex;

    #endregion

    #region 게임 관련 중요 오브젝트

    [Space(10)]
    [Header("게임 관련 중요 오브젝트")]
    public GameObject cameraObj;

    public AudioClip gameSound;

    #endregion

    [HideInInspector] public bool isGameStop;
    [HideInInspector] public bool isUseSkill;

    public float currentGold;

    private int curStageMaxWave;
    private int curWave;
    private int starCount;

    private float evolutionStoneCurHealth;

    private bool isShowEvolutionUpgradeMenu = false;
    
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
        
        // 변수 초기화
        getGoldMultiply = 1;
        towerAttackDamageMultiply = 1;
        skillAttackDamageMultiply = 1;
        skillCoolTimeMultiply = 1;

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        //변수 초기화
        currentGold = startGoldCount;
        evolutionStoneCurHealth = evolutionStoneMaxHealth;
        curStageMaxWave = 0;
        curWave = 0;
        gameClearMenu.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        gamePauseMenu.gameObject.SetActive(false);

        goldText.text = currentGold.ToString();
        
        SoundManager.Instance.PlayMusic(gameSound);
    }

    private void Update()
    {
        //goldText.text = currentGold.ToString();
        EvolutionHealthBarUpdate();
        GamePauseUpdate();
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
            GameOver();
        }
    }
    
    private void GameOver()
    {
        isGameStop = true;
        
        gameOverMenu.gameObject.SetActive(true);
    }

    public void SetCurStageMaxWave(int index)
    {
        if (index > curStageMaxWave)
        {
            curStageMaxWave = index;
            
            SetWaveText();
        }
    }
    
    public void NextWave()
    {
        curWave++;
        
        SetWaveText();
    }
    
    private void SetWaveText()
    {
        waveText.text = curWave + "/" + curStageMaxWave;
    }
    
    private void EvolutionHealthBarUpdate()
    {
        evolutionStoneHealthBar.fillAmount = Mathf.Lerp(evolutionStoneHealthBar.fillAmount,
            evolutionStoneCurHealth / evolutionStoneMaxHealth, Time.deltaTime * 12);
    }

    private void GamePauseUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameStop)
            {
                ShowGamePause();
            }
            else if (isGameStop)
            {
                HideGamePause();
            }
        }
    }
    
    private void ShowGamePause()
    {
        isGameStop = true;
        
        gamePauseMenu.gameObject.SetActive(true);
    }
    
    private void HideGamePause()
    {
        isGameStop = false;
        
        gamePauseMenu.CloseMenu();
    }

    public void GameClear()
    {
        isGameStop = true;
        
        if(evolutionStoneCurHealth > 0)
        {
            starCount++;
            PlayerPrefs.SetInt("Lv" + levelIndex, starCount);
        }

        if (evolutionStoneCurHealth >= evolutionStoneMaxHealth * 0.7f)
        {
            starCount++;
            PlayerPrefs.SetInt("Lv" + levelIndex, starCount);
        }

        if (evolutionStoneCurHealth == evolutionStoneMaxHealth)
        {
            starCount++;
            PlayerPrefs.SetInt("Lv" + levelIndex, starCount);
        }
        
        gameClearMenu.gameObject.SetActive(true);
        
        gameClearMenu.Init(starCount);
    }

    public void ShowEvolutionUpgradeMenu()
    {
        if (isShowEvolutionUpgradeMenu)
        {
            evolutionUpgradeMenu.GetComponent<RectTransform>().DOAnchorPosY(-500, 0.3f);

            isShowEvolutionUpgradeMenu = false;
        }
        else if (!isShowEvolutionUpgradeMenu)
        {
            evolutionUpgradeMenu.GetComponent<RectTransform>().DOAnchorPosY(0, 0.3f);

            isShowEvolutionUpgradeMenu = true;
        }
    }
}
