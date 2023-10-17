using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPosCheck : MonoBehaviour
{
    //public MagicTowerTemplate magicTemplate;
    //private GameManager playerGold;

    //public int Magiclevel = 0;
    //public GameObject Pos1;
    //public GameObject Pos2;
    //public GameObject Pos3;

    //private bool checkCoin = false;
    //public int TestCoste = 150;

    //private void Start()
    //{
    //    playerGold = FindObjectOfType<GameManager>();
    //    Debug.Log("Null" + playerGold.currentGold);

    //    // ���� �� ��� Pos ������Ʈ�� ��Ȱ��ȭ
    //    Pos1.SetActive(false);
    //    Pos2.SetActive(false);
    //    Pos3.SetActive(false);


    //    // Testcoin ����ŭ PlayerGold�� ó�� �� ���� ���ҽ�ŵ�ϴ�.
    //    if (!checkCoin)
    //    {
    //        Debug.Log("d");
    //        playerGold.currentGold -= TestCoste;
    //        checkCoin = true;
    //    }
    //}

    //private void Update()
    //{
    //    // ������ ���� ������ Pos ������Ʈ�� ���¸� ������Ʈ
    //    UpdatePosObjects();
    //}


    //public void UpgradeTower()
    //{
    //    if (Magiclevel < magicTemplate.mweapon.Length - 1)
    //    {
    //        if (playerGold.currentGold >= magicTemplate.mweapon[Magiclevel + 1].Mcost)
    //        {
    //            playerGold.currentGold -= magicTemplate.mweapon[Magiclevel + 1].Mcost;
    //            Magiclevel++;
    //            Debug.Log("Ÿ�� ���׷��̵�: ���� " + Magiclevel);
    //        }
    //        else
    //        {
    //            Debug.Log("�� �����ϴ�");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("�ִ� ���׷��̵� ���� �Դϴ�.");
    //    }
    //}

    //private void UpdatePosObjects()
    //{
    //    // ���� ������ ���� Pos ������Ʈ�� ���¸� ������Ʈ
    //    switch (Magiclevel)
    //    {
    //        case 0:
    //            Pos1.SetActive(true);
    //            Pos2.SetActive(false);
    //            Pos3.SetActive(false);
    //            break;
    //        case 1:
    //            Pos1.SetActive(true);
    //            Pos2.SetActive(true);
    //            Pos3.SetActive(false);
    //            break;
    //        case 2:
    //            Pos1.SetActive(true);
    //            Pos2.SetActive(true);
    //            Pos3.SetActive(true);
    //            break;

    //    }

    //}




    public MagicTowerTemplate magicTemplate;
    private GameManager playerGold;

    public int Magiclevel = 0;
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;

    private bool checkCoin = false;

    public bool isclick = false;

    public int Startingcoin = 150;

    private void Start()
    {
        playerGold = FindObjectOfType<GameManager>();
        Debug.Log("Null" + playerGold.currentGold);

        // ���� �� ��� Pos ������Ʈ�� ��Ȱ��ȭ
        Pos1.SetActive(false);
        Pos2.SetActive(false);
        Pos3.SetActive(false);


        // Testcoin ����ŭ PlayerGold�� ó�� �� ���� ���ҽ�ŵ�ϴ�.
        if (!checkCoin)
        {
            Debug.Log("d");
            playerGold.UseGold(magicTemplate.mweapon[Magiclevel].Mcost);
            checkCoin = true;
        }
    }

    private void Update()
    {
        // ������ ���� ������ Pos ������Ʈ�� ���¸� ������Ʈ
        UpdatePosObjects();

        if (isclick == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isclick = false;
            }
        }
    }


    public void UpgradeTower()
    {
        if (magicTemplate != null) // magicTemplate�� null�� �ƴ��� Ȯ��
        {
            if (Magiclevel < magicTemplate.mweapon.Length - 1)
            {
                if (playerGold.currentGold >= magicTemplate.mweapon[Magiclevel + 1].Mcost)
                {
                    //playerGold.currentGold -= magicTemplate.mweapon[Magiclevel + 1].Mcost;
                    //Magiclevel++;

                    float cost = magicTemplate.mweapon[Magiclevel + 1].Mcost;
                    playerGold.UseGold(cost);
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
        else
        {
            Debug.LogError("magicTemplate�� ����.");
        }
    }

    private void OnMouseDown()
    {
        isclick = true;
        Debug.Log("��ȣ �׿��� �ڴ�");
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
