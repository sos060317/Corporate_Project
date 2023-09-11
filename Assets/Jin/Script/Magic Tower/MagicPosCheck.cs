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

        // 시작 시 모든 Pos 오브젝트를 비활성화
        Pos1.SetActive(false);
        Pos2.SetActive(false);
        Pos3.SetActive(false);
    }

    private void Update()
    {
        // 레벨이 오를 때마다 Pos 오브젝트의 상태를 업데이트
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
