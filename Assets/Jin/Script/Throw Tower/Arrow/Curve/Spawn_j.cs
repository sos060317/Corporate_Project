using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_j : MonoBehaviour
{
    #region 쓰레기
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

    //public bool isClicked = false; // 클릭 상태를 저장하는 변수

    //private void Start()
    //{
    //    // 게임 시작 시 RoundObject를 비활성화합니다.
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
    //            // 새로운 오브젝트가 리스트에 추가되면 FlowingObject를 활성화
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
    //            if (FlowingObject != null && FlowingObject.activeSelf)
    //            {
    //                UpdateFlowingObjectPosition(); // FlowingObject 위치 업데이트
    //                SpawnArrowsAtPositions();
    //            }
    //        }
    //    }
    //    else
    //    { 
    //        // 리스트가 비어 있으면 FlowingObject를 비활성화
    //        if (FlowingObject != null)
    //        {
    //            FlowingObject.SetActive(false);
    //        }
    //    }

    //    if (isClicked == true)
    //    {
    //        if (Input.GetMouseButtonDown(1))
    //        {
    //            RoundObject.SetActive(false);
    //            isClicked = false;
    //        }
    //    }


    //    if (isClicked && alevel == 3)
    //    {
    //        detectionRadius = 9;
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    // 이 오브젝트를 클릭할 때만 isClicked를 true로 설정하여 범위를 활성화합니다.
    //    isClicked = true;
    //    //버튼이 나오는 코드
    //    RoundObject.SetActive(true);
    //}

    //private void UpdateFlowingObjectPosition()
    //{
    //    if (enemyList.Count > 0)
    //    {
    //        // 첫 번째 오브젝트만 따라가도록 수정
    //        GameObject firstEnemy = enemyList[0];
    //        Vector2 targetPosition = firstEnemy.transform.position;
    //        FlowingObject.transform.position = Vector2.MoveTowards(FlowingObject.transform.position, targetPosition, followSpeed * Time.deltaTime);
    //    }
    //}

    //private void SpawnArrowsAtPositions()
    //{
    //    foreach (GameObject spawnPosition in spawnPositions)
    //    {
    //        if (spawnPosition.activeSelf) // 오브젝트가 활성화되어 있는 경우에만 소환합니다.
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

    //
    #endregion



    //public GameObject RoundObject;
    //public ArrowTowerTemplate arrowTemplate;
    //public UpgradeArrowTower sp;
    //private int alevel = 0;

    //public GameObject arrowPrefab;
    //public List<GameObject> spawnPositions;
    //public string enemyTag = "Enemy";
    //public float detectionRadius = 5.0f;
    ////public GameObject FlowingObject;
    ////public float followSpeed = 5.0f;
    //public float spawnInterval = 1.5f;

    //[SerializeField]
    //private List<GameObject> enemyList = new List<GameObject>();
    //private bool canSpawn = false;
    //public float timeSinceLastSpawn = 0f;

    //public bool isClicked = false; // 클릭 상태를 저장하는 변수

    //// 
    //public Vector2 EnemyPos;
    //public bool nowShot = false;





    //private void Start()
    //{
    //    RoundObject.SetActive(false);
    //}

    //private void Update()
    //{
    //    alevel = sp.Arrowlevel;

    //    Collider2D colliders = Physics2D.OverlapCircle(transform.position, detectionRadius);

    //    float radius = arrowTemplate.aweapon[alevel].Aradius;
    //    RoundObject.transform.localScale = new Vector3(radius, radius, radius);



    //    enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position) || !enemy.activeSelf);

    //    canSpawn = enemyList.Count > 0;



    //    if (isClicked == true)
    //    {
    //        if (Input.GetMouseButtonDown(1))
    //        {
    //            RoundObject.SetActive(false);
    //            isClicked = false;
    //        }
    //    }


    //    if (isClicked && alevel == 3)
    //    {
    //        detectionRadius = 9;
    //    }

    //    //=====================

    //    timeSinceLastSpawn += Time.deltaTime;

    //    if (enemyList.Count > 0 && timeSinceLastSpawn >= spawnInterval)
    //    {
    //        GameObject firstEnemy = enemyList[0];
    //        EnemyPos = firstEnemy.transform.position;
    //        nowShot = true;


    //        SpawnArrowsAtPositions();
    //        timeSinceLastSpawn = 0f;
    //    }
    //    else
    //    {
    //        nowShot = false;
    //        EnemyPos = Vector2.zero;
    //    }

    //}

    //private void OnMouseDown()
    //{
    //    // 이 오브젝트를 클릭할 때만 isClicked를 true로 설정하여 범위를 활성화합니다.
    //    isClicked = true;
    //    //버튼이 나오는 코드
    //    RoundObject.SetActive(true);
    //}


    //private void SpawnArrowsAtPositions()
    //{
    //    foreach (GameObject spawnPosition in spawnPositions)
    //    {
    //        if (spawnPosition.activeSelf) 
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


    public GameObject RoundObject;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower sp;
    private int alevel = 0;

    public GameObject arrowPrefab;
    public List<GameObject> spawnPositions;
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    //public GameObject FlowingObject;
    //public float followSpeed = 5.0f;
    public float spawnInterval = 1.5f;

    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    private float timeSinceLastSpawn = 0f;

    public bool isClicked = false; // 클릭 상태를 저장하는 변수

    //================

    public Vector2 EnemyPos;
    public bool nowShot = false;

    private void Start()
    {
        // 게임 시작 시 RoundObject를 비활성화합니다.
        RoundObject.SetActive(false);
    }

    private void Update()
    {
        alevel = sp.Arrowlevel;

        List<Collider2D> colliders = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, detectionRadius));

        float radius = arrowTemplate.aweapon[alevel].Aradius;
        RoundObject.transform.localScale = new Vector3(radius, radius, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag) && !enemyList.Contains(collider.gameObject))
            {
                enemyList.Add(collider.gameObject);
            }
        }

        enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position) || !enemy.activeSelf);

        nowShot = enemyList.Count > 0;

        if (nowShot)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                timeSinceLastSpawn = 0f;


                UpdateFlowingObjectPosition(); // FlowingObject 위치 업데이트
                SpawnArrowsAtPositions();

            }
        }
        else
        {

        }

        if (isClicked == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RoundObject.SetActive(false);
                isClicked = false;
            }
        }

        if (enemyList.Count > 0)
        {
            GameObject firstEnemy = enemyList[0];
            EnemyPos = firstEnemy.transform.position;
            nowShot = true;
        }
        else
        {
            nowShot = false;
            EnemyPos = Vector2.zero;
        }



        if (isClicked && alevel == 3)
        {
            detectionRadius = 9;
        }
    }

    private void OnMouseDown()
    {
        // 이 오브젝트를 클릭할 때만 isClicked를 true로 설정하여 범위를 활성화합니다.
        isClicked = true;
        //버튼이 나오는 코드
        RoundObject.SetActive(true);
    }

    private void UpdateFlowingObjectPosition()
    {
        if (enemyList.Count > 0)
        {
            // 첫 번째 오브젝트만 따라가도록 수정
            GameObject firstEnemy = enemyList[0];
            Vector2 targetPosition = firstEnemy.transform.position;
        }
    }

    private void SpawnArrowsAtPositions()
    {
        foreach (GameObject spawnPosition in spawnPositions)
        {
            if (spawnPosition.activeSelf) // 오브젝트가 활성화되어 있는 경우에만 소환합니다.
            {
                var temp = Instantiate(arrowPrefab, spawnPosition.transform.position, Quaternion.identity).GetComponent<TrowingObject>();
                temp.arrowTower = GetComponent<Spawn_j>();
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
