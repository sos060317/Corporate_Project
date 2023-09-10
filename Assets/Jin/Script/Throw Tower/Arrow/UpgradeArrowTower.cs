using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArrowTower : MonoBehaviour
{
    public ArrowTowerTemplate arrowTemplate;
    private PlayerGold playerGold;

    public int Arrowlevel = 0; //�Ƹ�? ������ 3?
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;

    private void Start()
    {
        playerGold = FindObjectOfType<PlayerGold>();
        Debug.Log("Null" + playerGold.currentGold);
        
        // ���� �� ��� Pos ������Ʈ�� ��Ȱ��ȭ
        Pos1.SetActive(false);
        Pos2.SetActive(false);
        Pos3.SetActive(false);
    }

    private void Update()
    {
        // ������ ���� ������ Pos ������Ʈ�� ���¸� ������Ʈ
        UpdatePosObjects();
    }


    public void UpgradeTower()
    {
        if (Arrowlevel < arrowTemplate.aweapon.Length - 1)
        {
            if (playerGold.currentGold >= arrowTemplate.aweapon[Arrowlevel + 1].Acost)
            {
                playerGold.currentGold -= arrowTemplate.aweapon[Arrowlevel + 1].Acost;
                Arrowlevel++;
                Debug.Log("Ÿ�� ���׷��̵�: ���� " + Arrowlevel);
            }
            else
            {
                Debug.Log("�� �����ϴ�");
            }
        }
        else
        {
            Debug.Log("�ִ� ���׷��̵� ���� �Դϴ�.");
        }
    }

    private void UpdatePosObjects()
    {
        // ���� ������ ���� Pos ������Ʈ�� ���¸� ������Ʈ
        switch (Arrowlevel)
        {
            case 0:
                Pos1.SetActive(true);
                Pos2.SetActive(false);
                Pos3.SetActive(false);
                break;
            case 1:
                Pos1.SetActive(true);
                Pos2.SetActive(true);
                Pos3.SetActive(false);
                break;
            case 2:
                Pos1.SetActive(true);
                Pos2.SetActive(true);
                Pos3.SetActive(true);
                break;

        }

    }
}
