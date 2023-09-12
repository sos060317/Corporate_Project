using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGold : MonoBehaviour
{
    //[SerializeField]
    //public int currentGold = 1000;

    //public int CurrentGold
    //{
    //    set => currentGold = Mathf.Max(0, value);
    //    get => currentGold;
    //}

    [SerializeField]
    public int currentGold = 1000;

    public int CurrentGold
    {
        set
        {
            currentGold = Mathf.Max(0, value);
            UpdateGoldText(); // 값이 변경될 때마다 텍스트 업데이트
            CheckAndAddGoldOnEnemyDeath(); // 값이 변경될 때마다 적 죽을 때 골드 추가 체크
        }
        get => currentGold;
    }

    public TextMeshProUGUI goldText; // UI 텍스트 오브젝트를 연결할 변수

    private void Update()
    {
        // 게임 시작 시 텍스트 초기화
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = "" + currentGold.ToString(); // 현재 Gold 값을 텍스트에 표시
        }
    }

    
    private void CheckAndAddGoldOnEnemyDeath()
    {
        int deductionAmount = 50;
        if (deductionAmount > 0)
        {
            CurrentGold += deductionAmount;
        }
    }

}
