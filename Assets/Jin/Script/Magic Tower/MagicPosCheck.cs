using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPosCheck : MonoBehaviour
{
    public MagicTowerTemplate magicTemplate;
    private PlayerGold playerGold;

    public int Magiclevel = 0;
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
        if (Magiclevel < magicTemplate.mweapon.Length - 1)
        {
            if (playerGold.currentGold >= magicTemplate.mweapon[Magiclevel + 1].Mcost)
            {
                playerGold.currentGold -= magicTemplate.mweapon[Magiclevel + 1].Mcost;
                Magiclevel++;
                Debug.Log("Ÿ�� ���׷��̵�: ���� " + Magiclevel);
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
        switch (Magiclevel)
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
