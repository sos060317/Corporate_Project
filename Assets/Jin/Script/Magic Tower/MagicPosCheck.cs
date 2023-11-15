using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class MagicPosCheck : MonoBehaviour
{
    public TextMeshProUGUI UpgradeCostText;
    public TextMeshProUGUI DestroyCost;

    public MagicWindow MagicUI;
    public Animator MagicAnimation;

    public MagicTowerTemplate magicTemplate;
    //private GameManager playerGold;

    public GameObject RoundObj;

    public int Magiclevel = 0;
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;

    private bool checkCoin = false;

    public bool isclick = false;

    public int Startingcoin = 150;

    private bool MToggle;

    private void Start()
    {
        RoundObj.SetActive(false);

        //playerGold = FindObjectOfType<GameManager>();
        //Debug.Log("Null" + playerGold.currentGold);

        // ���� �� ��� Pos ������Ʈ�� ��Ȱ��ȭ
        Pos1.SetActive(false);
        Pos2.SetActive(false);
        Pos3.SetActive(false);


        // Testcoin ����ŭ PlayerGold�� ó�� �� ���� ���ҽ�ŵ�ϴ�.
        if (!checkCoin)
        {
            //Debug.Log("d");
            GameManager.Instance.UseGold(magicTemplate.mweapon[Magiclevel].Mcost);
            checkCoin = true;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            MagicAnimation.StartPlayback();
            return;
        }
        else
        {
            MagicAnimation.StopPlayback();
        }

        UpdateCostText();
        DestroyCostText();

        if (Magiclevel == 1)
        {
            MagicAnimation.SetBool("Level1", true);
        }

        float roundSize = magicTemplate.mweapon[Magiclevel].Mradius;
        RoundObj.transform.localScale = new Vector3(roundSize * 2, roundSize * 2, roundSize * 2);


        // ������ ���� ������ Pos ������Ʈ�� ���¸� ������Ʈ
        UpdatePosObjects();

        if (MagicUI.buttonDown == true)
        {
            isclick = false;
            RoundObj.SetActive(false);
            MagicUI.buttonDown = false;
        }

        StartCoroutine(DelayTm());
       
        if (isclick == true && MToggle == true)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isclick = false;
                    RoundObj.SetActive(false);
                    MToggle = false;
                }
            }
        }
    }

    public void DestroyTower()
    {
        Destroy(gameObject);
        GameManager.Instance.GetGold(magicTemplate.mweapon[Magiclevel].ResellCost);
    }

    private void UpdateCostText()
    {
        if (Magiclevel <= 2)
        {
            if (UpgradeCostText != null)
            {
                UpgradeCostText.text = magicTemplate.mweapon[Magiclevel + 1].Mcost.ToString();
            }
        }
        else
        {
            UpgradeCostText.text = ("Max!");
        }
        
    }

    private void DestroyCostText()
    {
        // costText�� null�� �ƴ϶�� TMP �ؽ�Ʈ ������Ʈ
        if (DestroyCost != null)
        {
            DestroyCost.text = magicTemplate.mweapon[Magiclevel].ResellCost.ToString();
        }
    }


    public void UpgradeTower()
    {
        if (magicTemplate != null) // magicTemplate�� null�� �ƴ��� Ȯ��
        {
            if (Magiclevel < magicTemplate.mweapon.Length - 1)
            {
                if (GameManager.Instance.currentGold >= magicTemplate.mweapon[Magiclevel + 1].Mcost)
                {
                    //playerGold.currentGold -= magicTemplate.mweapon[Magiclevel + 1].Mcost;
                    //Magiclevel++;

                    float cost = magicTemplate.mweapon[Magiclevel + 1].Mcost;
                    GameManager.Instance.UseGold(cost);
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        //Debug.Log("Ŭ�� ýũ");
        isclick = true;
        RoundObj.SetActive(true);
        MToggle = false;
    }

    private IEnumerator DelayTm()
    {
        yield return new WaitForSeconds(0.2f);
        MToggle = true;
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
