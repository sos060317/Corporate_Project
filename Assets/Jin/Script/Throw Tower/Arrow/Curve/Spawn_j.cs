using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Spawn_j : MonoBehaviour
{
    //public AroRoundCheck ArrowRound;

    //public GameObject RoundObject;
    //public ArrowTowerTemplate arrowTemplate;
    //public UpgradeArrowTower sp;
    //private int alevel = 0;

    //public GameObject arrowPrefab;
    //public List<GameObject> spawnPositions;
    //public string enemyTag = "Enemy";
    //public float detectionRadius = 5.0f;
    //public float spawnInterval = 1.5f;

    //[SerializeField]
    //private List<GameObject> enemyList = new List<GameObject>();
    //private float timeSinceLastSpawn = 1f;

    //public bool isClicked = false;

    //public Vector2 EnemyPos;
    //public bool nowShot = false;
    //public bool canToggle = true;


    //private void Start()
    //{
    //    RoundObject.SetActive(false);

    //    if (ArrowRound != null)
    //    {
    //        detectionRadius = ArrowRound.AroRound;
    //    }

    //}

    //private void Update()
    //{

    //    //Debug.Log(enemyList.Count); 적 수 확인

    //    alevel = sp.Arrowlevel;

    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(ArrowRound.transform.position, ArrowRound.AroRound);

    //    float radius = arrowTemplate.aweapon[alevel].Aradius;
    //    RoundObject.transform.localScale = new Vector3(radius, radius, radius);

    //    foreach (Collider2D collider in colliders)
    //    {
    //        if (collider.CompareTag(enemyTag) && !enemyList.Contains(collider.gameObject))
    //        {
    //            enemyList.Add(collider.gameObject);
    //        }
    //    }

    //    enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position) || !enemy.activeSelf);

    //    nowShot = enemyList.Count > 0;

    //    if (nowShot)
    //    {
    //        timeSinceLastSpawn += Time.deltaTime;
    //        if (timeSinceLastSpawn >= spawnInterval)
    //        {
    //            timeSinceLastSpawn = 0f;
    //            UpdateFlowingObjectPosition();
    //            SpawnArrowsAtPositions();
    //        }
    //    }
    //    else
    //    {

    //    }

    //    StartCoroutine(DelayTm());

    //    if (isClicked == true && canToggle == true)
    //    {

    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            isClicked = false;
    //            RoundObject.SetActive(false);
    //            canToggle = false;
    //        }
    //    }


    //    if (enemyList.Count > 0)
    //    {
    //        GameObject firstEnemy = enemyList[0];
    //        EnemyPos = firstEnemy.transform.position;
    //        nowShot = true;

    //    }
    //    else
    //    {
    //        nowShot = false;
    //        EnemyPos = Vector2.zero;
    //    }


    //    if (alevel == 3)
    //    {
    //        detectionRadius = 9f;
    //    }
    //}

    //private IEnumerator DelayTm()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    canToggle = true;
    //}

    //private void OnMouseDown()
    //{
    //    if (!isClicked && canToggle == true)
    //    {
    //        isClicked = true;
    //        RoundObject.SetActive(true);
    //        canToggle = false;
    //    }
    //}

    //private void UpdateFlowingObjectPosition()
    //{
    //    if (enemyList.Count > 0)
    //    {

    //        GameObject firstEnemy = enemyList[0];
    //        Vector2 targetPosition = firstEnemy.transform.position;
    //    }
    //}

    //private void SpawnArrowsAtPositions()
    //{
    //    foreach (GameObject spawnPosition in spawnPositions)
    //    {
    //        if (spawnPosition.activeSelf)
    //        {
    //            var temp = Instantiate(arrowPrefab, spawnPosition.transform.position, Quaternion.identity).GetComponent<TrowingObject>();

    //            temp.arrowTower = GetComponent<Spawn_j>();
    //        }
    //    }
    //}

    //private bool IsWithinRadius(Vector2 position)
    //{
    //    return Vector2.Distance(ArrowRound.transform.position, position) <= detectionRadius;  //  범위 안에 Enmey 위치 찾기?
    //}

    //private void OnDrawGizmosSelected()
    //{ 

    //    if (ArrowRound != null)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(ArrowRound.transform.position, ArrowRound.AroRound);
    //    }
    //}

    public AroRoundCheck ArrowRound;
    public ArrowWindow ArrowTowerUI;

    public GameObject RoundObject;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower sp;
    private int alevel = 0;

    public GameObject arrowPrefab;
    public List<GameObject> spawnPositions;
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    public float spawnInterval = 1.5f;

    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    private float timeSinceLastSpawn = 1f;

    public bool isClicked = false;

    public Vector2 EnemyPos;
    public bool nowShot = false;
    public bool canToggle = true;


    private void Start()
    {
        RoundObject.SetActive(false);

        if (ArrowRound != null)
        {
            detectionRadius = ArrowRound.AroRound;
        }

    }

    private void Update()
    {

        //Debug.Log(enemyList.Count); 적 수 확인

        alevel = sp.Arrowlevel;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(ArrowRound.transform.position, ArrowRound.AroRound);

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
                UpdateFlowingObjectPosition();
                SpawnArrowsAtPositions();
            }
        }
        else
        {

        }

        if(ArrowTowerUI.buttonDown == true)
        {
            isClicked = false;
            RoundObject.SetActive(false);
            ArrowTowerUI.buttonDown = false;
        }

        StartCoroutine(DelayTm());

        if (isClicked == true && canToggle == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    isClicked = false;
                    RoundObject.SetActive(false);
                    canToggle = false;
                }
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


        if (alevel == 3)
        {
            detectionRadius = 9f;
        }
    }

    private IEnumerator DelayTm()
    {
        yield return new WaitForSeconds(0.2f);
        canToggle = true;
    }

    private void OnMouseDown()
    {
        if (!isClicked && canToggle == true)
        {
            isClicked = true;
            RoundObject.SetActive(true);
            canToggle = false;
        }
    }

    private void UpdateFlowingObjectPosition()
    {
        if (enemyList.Count > 0)
        {

            GameObject firstEnemy = enemyList[0];
            Vector2 targetPosition = firstEnemy.transform.position;
        }
    }

    private void SpawnArrowsAtPositions()
    {
        foreach (GameObject spawnPosition in spawnPositions)
        {
            if (spawnPosition.activeSelf)
            {
                var temp = Instantiate(arrowPrefab, spawnPosition.transform.position, Quaternion.identity).GetComponent<TrowingObject>();

                temp.arrowTower = GetComponent<Spawn_j>();
            }
        }
    }

    private bool IsWithinRadius(Vector2 position)
    {
        return Vector2.Distance(ArrowRound.transform.position, position) <= detectionRadius;  //  범위 안에 Enmey 위치 찾기?
    }

    private void OnDrawGizmosSelected()
    {

        if (ArrowRound != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(ArrowRound.transform.position, ArrowRound.AroRound);
        }
    }
}
