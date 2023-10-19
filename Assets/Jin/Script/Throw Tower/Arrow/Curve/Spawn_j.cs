using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_j : MonoBehaviour
{
    #region 지랄 코드
    
    //public GameObject RoundObject;
    //public ArrowTowerTemplate arrowTemplate;
    //public UpgradeArrowTower sp;
    //private int alevel = 0;

    //public GameObject arrowPrefab;
    //public List<GameObject> spawnPositions;
    //public string enemyTag = "Enemy";
    //public float detectionRadius = 5.0f;
    //public GameObject FlowingObject;
    //public float followSpeed = 5.0f;
    //public float spawnInterval = 1.5f;

    //[SerializeField]
    //private List<GameObject> enemyList = new List<GameObject>();
    //private bool canSpawn = false;
    //private float timeSinceLastSpawn = 0f;

    //private bool isClicked = false; // Ŭ�� ���¸� �����ϴ� ����

    //private void Start()
    //{
    //    // ���� ���� �� RoundObject�� ��Ȱ��ȭ�մϴ�.
    //    RoundObject.SetActive(false);
    //}

    //private void Update()
    //{
    //    alevel = sp.Arrowlevel;

    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

    //    float radius = arrowTemplate.aweapon[alevel].Aradius;
    //    RoundObject.transform.localScale = new Vector3(radius, radius, radius);

    //    foreach (Collider2D collider in colliders)
    //    {
    //        if (collider.CompareTag(enemyTag) && !enemyList.Contains(collider.gameObject))
    //        {
    //            enemyList.Add(collider.gameObject);
    //            // ���ο� ������Ʈ�� ����Ʈ�� �߰��Ǹ� FlowingObject�� Ȱ��ȭ
    //            if (FlowingObject != null)
    //            {
    //                FlowingObject.SetActive(true);
    //            }
    //        }
    //    }

    //    enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position) || !enemy.activeSelf);

    //    canSpawn = enemyList.Count > 0;

    //    if (canSpawn)
    //    {
    //        timeSinceLastSpawn += Time.deltaTime;
    //        if (timeSinceLastSpawn >= spawnInterval)
    //        {
    //            timeSinceLastSpawn = 0f;
    //            if (FlowingObject != null && FlowingObject.activeSelf && isClicked)
    //            {
    //                SpawnArrowsAtPositions();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        // ����Ʈ�� ��� ������ FlowingObject�� ��Ȱ��ȭ
    //        if (FlowingObject != null)
    //        {
    //            FlowingObject.SetActive(false);
    //        }
    //    }

    //    if(Input.GetMouseButtonDown(1))
    //    {
    //        RoundObject.SetActive(false);
    //    }

    //    if (isClicked && alevel == 3)
    //    {
    //        detectionRadius = 9;
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    // �� ������Ʈ�� Ŭ���� ���� isClicked�� true�� �����Ͽ� ������ Ȱ��ȭ�մϴ�.
    //    isClicked = true;
    //    RoundObject.SetActive(true);
    //}

    //private void SpawnArrowsAtPositions()
    //{
    //    foreach (GameObject spawnPosition in spawnPositions)
    //    {
    //        if (spawnPosition.activeSelf) // ������Ʈ�� Ȱ��ȭ�Ǿ� �ִ� ��쿡�� ��ȯ�մϴ�.
    //        {
    //            Instantiate(arrowPrefab, spawnPosition.transform.position, Quaternion.identity);
    //        }
    //    }
    //}

    //private bool IsWithinRadius(Vector2 position)
    //{
    //    return Vector2.Distance(transform.position, position) <= detectionRadius;
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
    //}

    //=======================================================================================================
    
    #endregion

    public GameObject RoundObject;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower sp;
    private int alevel = 0;

    public GameObject arrowPrefab;
    public List<GameObject> spawnPositions;
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    public GameObject FlowingObject;
    public float followSpeed = 5.0f;
    public float spawnInterval = 1.5f;

    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    private bool canSpawn = false;
    private float timeSinceLastSpawn = 0f;

    public bool isClicked = false; // Ŭ�� ���¸� �����ϴ� ����

    private void Start()
    {
        // ���� ���� �� RoundObject�� ��Ȱ��ȭ�մϴ�.
        RoundObject.SetActive(false);
    }

    private void Update()
    {
        alevel = sp.Arrowlevel;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        float radius = arrowTemplate.aweapon[alevel].Aradius;
        RoundObject.transform.localScale = new Vector3(radius, radius, radius);

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
                if (FlowingObject != null && FlowingObject.activeSelf)
                {
                    UpdateFlowingObjectPosition(); // FlowingObject ��ġ ������Ʈ
                    SpawnArrowsAtPositions();
                }
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

        if (isClicked == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RoundObject.SetActive(false);
                isClicked = false;
            }
        }


        if (isClicked && alevel == 3)
        {
            detectionRadius = 9;
        }
    }

    private void OnMouseDown()
    {
        // �� ������Ʈ�� Ŭ���� ���� isClicked�� true�� �����Ͽ� ������ Ȱ��ȭ�մϴ�.
        isClicked = true;
        //��ư�� ������ �ڵ�
        RoundObject.SetActive(true);
    }

    private void UpdateFlowingObjectPosition()
    {
        if (enemyList.Count > 0)
        {
            // ù ��° ������Ʈ�� ���󰡵��� ����
            GameObject firstEnemy = enemyList[0];
            Vector2 targetPosition = firstEnemy.transform.position;
            FlowingObject.transform.position = Vector2.MoveTowards(FlowingObject.transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private void SpawnArrowsAtPositions()
    {
        foreach (GameObject spawnPosition in spawnPositions)
        {
            if (spawnPosition.activeSelf) // ������Ʈ�� Ȱ��ȭ�Ǿ� �ִ� ��쿡�� ��ȯ�մϴ�.
            {
                Instantiate(arrowPrefab, spawnPosition.transform.position, Quaternion.identity);
            }
        }
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
