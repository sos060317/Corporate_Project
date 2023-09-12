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
            UpdateGoldText(); // ���� ����� ������ �ؽ�Ʈ ������Ʈ
            CheckAndAddGoldOnEnemyDeath(); // ���� ����� ������ �� ���� �� ��� �߰� üũ
        }
        get => currentGold;
    }

    public TextMeshProUGUI goldText; // UI �ؽ�Ʈ ������Ʈ�� ������ ����

    private void Update()
    {
        // ���� ���� �� �ؽ�Ʈ �ʱ�ȭ
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = "" + currentGold.ToString(); // ���� Gold ���� �ؽ�Ʈ�� ǥ��
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
