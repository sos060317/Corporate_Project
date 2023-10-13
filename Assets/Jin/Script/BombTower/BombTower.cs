using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : MonoBehaviour
{
    private GameManager playerGold;
    public BombTowerTemplate bombTemplate; 


    public GameObject arrowPrefab;
    public GameObject spawnPos;
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    public GameObject FlowingObject;
    public float followSpeed = 5.0f;
    public float spawnInterval = 1.5f; // �߻� �ӵ�?

    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    private bool canSpawn = false;
    private float timeSinceLastSpawn = 0f;

    private int BombLevel = 0;

    private bool checkCoin = false;

    public bool isClick = false;


    private void Start()
    {

        playerGold = FindObjectOfType<GameManager>();
        // Testcoin ����ŭ PlayerGold�� ó�� �� ���� ���ҽ�ŵ�ϴ�.
        if (!checkCoin)
        {
            Debug.Log("d");
            playerGold.UseGold(bombTemplate.Bweapon[BombLevel].Bcost);
            checkCoin = true;
        }
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag) && !enemyList.Contains(collider.gameObject))
            {
                enemyList.Add(collider.gameObject);
                // ���ο� ������Ʈ�� ����Ʈ�� �߰��Ǹ� FlowingObject�� Ȱ��ȭ
                if (FlowingObject != null)
                {
                    FlowingObject.SetActive(true);
                }
            }
        }

        enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position) || !enemy.activeSelf);

        canSpawn = enemyList.Count > 0;

        if (canSpawn)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                timeSinceLastSpawn = 0f;
                SpawnArrow();
            }
        }
        else
        {
            // ����Ʈ�� ��� ������ FlowingObject�� ��Ȱ��ȭ
            if (FlowingObject != null)
            {
                FlowingObject.SetActive(false);
            }
        }

        if (FlowingObject != null && enemyList.Count > 0)
        {
            Vector2 targetPosition = enemyList[0].transform.position;
            FlowingObject.transform.position = Vector2.MoveTowards(FlowingObject.transform.position, targetPosition, followSpeed * Time.deltaTime);
        }

        if (isClick == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isClick = false;
                Debug.Log("Ŭ�� false");
            }
        }
    }

    
    public void UpgradeTower()
    {
        if(bombTemplate != null)
        {
            if(BombLevel < bombTemplate.Bweapon.Length - 1)
            {
                if(playerGold.currentGold >= bombTemplate.Bweapon[BombLevel + 1].Bcost)
                {
                    float cost = bombTemplate.Bweapon[BombLevel + 1].Bcost;
                    playerGold.UseGold(cost);
                    BombLevel++;
                    Debug.Log("Ÿ�� ���׷��̵� : ���� " + BombLevel);
                }
                else
                {
                    Debug.Log("���� ����");
                }
            }
            else
            {
                Debug.Log("�ִ� ���׷��̵� �����Դϴ�");
            }
        }
        else
        {
            Debug.Log("Templat�� ����");
        }
    }

    private void OnMouseDown()
    {
        isClick = true;
        Debug.Log("Ŭ��Ȯ��");
    }

    private void SpawnArrow()
    {
        Instantiate(arrowPrefab, spawnPos.transform.position, Quaternion.identity);
    }

    private bool IsWithinRadius(Vector2 position)
    {
        return Vector2.Distance(transform.position, position) <= detectionRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
