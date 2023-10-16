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

    //    // 시작 시 모든 Pos 오브젝트를 비활성화
    //    Pos1.SetActive(false);
    //    Pos2.SetActive(false);
    //    Pos3.SetActive(false);


    //    // Testcoin 값만큼 PlayerGold를 처음 한 번만 감소시킵니다.
    //    if (!checkCoin)
    //    {
    //        Debug.Log("d");
    //        playerGold.currentGold -= TestCoste;
    //        checkCoin = true;
    //    }
    //}

    //private void Update()
    //{
    //    // 레벨이 오를 때마다 Pos 오브젝트의 상태를 업데이트
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
    //            Debug.Log("타워 업그레이드: 레벨 " + Magiclevel);
    //        }
    //        else
    //        {
    //            Debug.Log("돈 부족하다");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("최대 업그레이드 상태 입니다.");
    //    }
    //}

    //private void UpdatePosObjects()
    //{
    //    // 현재 레벨에 따라 Pos 오브젝트의 상태를 업데이트
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

        // 시작 시 모든 Pos 오브젝트를 비활성화
        Pos1.SetActive(false);
        Pos2.SetActive(false);
        Pos3.SetActive(false);


        // Testcoin 값만큼 PlayerGold를 처음 한 번만 감소시킵니다.
        if (!checkCoin)
        {
            Debug.Log("d");
            playerGold.UseGold(magicTemplate.mweapon[Magiclevel].Mcost);
            checkCoin = true;
        }
    }

    private void Update()
    {
        // 레벨이 오를 때마다 Pos 오브젝트의 상태를 업데이트
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
        if (magicTemplate != null) // magicTemplate이 null이 아닌지 확인
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
                    Debug.Log("타워 업그레이드: 레벨 " + Magiclevel);
                }
                else
                {
                    Debug.Log("돈 부족하다");
                }
            }
            else
            {
                Debug.Log("최대 업그레이드 상태 입니다.");
            }
        }
        else
        {
            Debug.LogError("magicTemplate이 없다.");
        }
    }

    private void OnMouseDown()
    {
        isclick = true;
        Debug.Log("성호 죽여야 겠다");
    }


    private void UpdatePosObjects()
    {
        // 현재 레벨에 따라 Pos 오브젝트의 상태를 업데이트
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
