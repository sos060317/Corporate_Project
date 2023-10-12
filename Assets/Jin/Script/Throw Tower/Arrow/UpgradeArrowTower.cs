using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArrowTower : MonoBehaviour
{
    //public ArrowTowerTemplate arrowTemplate;
    //private GameManager playerGold;

    //public int Arrowlevel = 0; // 아마? 만렙은 3?
    //public GameObject Pos1;
    //public GameObject Pos2;
    //public GameObject Pos3;

    //private bool hasSubtractedGold = false; // 이미 감소했는지 여부를 나타내는 변수
    //public int Testcoin = 50; // 테스트용 코인 변수

    //private void Start()
    //{
    //    playerGold = FindObjectOfType<GameManager>();
    //    Debug.Log("Null" + playerGold.currentGold);

    //    // 시작 시 모든 Pos 오브젝트를 비활성화
    //    Pos1.SetActive(false);
    //    Pos2.SetActive(false);
    //    Pos3.SetActive(false);

    //    // Testcoin 값만큼 PlayerGold를 처음 한 번만 감소시킵니다.
    //    if (!hasSubtractedGold)
    //    {
    //        Debug.Log("d");
    //        playerGold.currentGold -= Testcoin;
    //        hasSubtractedGold = true;
    //    }
    //}

    //private void Update()
    //{
    //    // 레벨이 오를 때마다 Pos 오브젝트의 상태를 업데이트
    //    UpdatePosObjects();
    //}

    //public void UpgradeTower()
    //{
    //    if (Arrowlevel < arrowTemplate.aweapon.Length - 1)
    //    {
    //        if (playerGold.currentGold >= arrowTemplate.aweapon[Arrowlevel + 1].Acost)
    //        {
    //            playerGold.currentGold -= arrowTemplate.aweapon[Arrowlevel + 1].Acost;    // 돈계산 
    //            Arrowlevel++;
    //            Debug.Log("타워 업그레이드: 레벨 " + Arrowlevel);
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
    //    switch (Arrowlevel)
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




    public ArrowTowerTemplate arrowTemplate;
    private GameManager playerGold;

    public int Arrowlevel = 0; // 아마? 만렙은 3?
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;

    private bool hasSubtractedGold = false; // 이미 감소했는지 여부를 나타내는 변수
    public int Testcoin = 50; // 테스트용 코인 변수

    private void Start()
    {
        playerGold = FindObjectOfType<GameManager>();
        Debug.Log("Null" + playerGold.currentGold);

        // 시작 시 모든 Pos 오브젝트를 비활성화
        Pos1.SetActive(false);
        Pos2.SetActive(false);
        Pos3.SetActive(false);

        // Testcoin 값만큼 PlayerGold를 처음 한 번만 감소시킵니다.
        if (!hasSubtractedGold)
        {
            Debug.Log("d");
            //playerGold.currentGold -= Testcoin;
            playerGold.UseGold(Testcoin);
            hasSubtractedGold = true;
        }
    }

    private void Update()
    {
        // 레벨이 오를 때마다 Pos 오브젝트의 상태를 업데이트
        UpdatePosObjects();
    }

    public void UpgradeTower()
    {
        if (Arrowlevel < arrowTemplate.aweapon.Length - 1)
        {
            if (playerGold.currentGold >= arrowTemplate.aweapon[Arrowlevel + 1].Acost)
            {
                // Acost만큼의 골드를 소비하는 부분을 UseGold 함수를 호출하여 수정하도록 바꿈
                float cost = arrowTemplate.aweapon[Arrowlevel + 1].Acost;
                playerGold.UseGold(cost);

                Arrowlevel++;
                Debug.Log("타워 업그레이드: 레벨 " + Arrowlevel);
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

    private void UpdatePosObjects()
    {
        // 현재 레벨에 따라 Pos 오브젝트의 상태를 업데이트
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