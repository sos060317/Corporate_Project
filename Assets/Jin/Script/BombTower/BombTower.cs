using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BombTower : MonoBehaviour
{
    public BombWindow BombTowerUI;

    private GameManager playerGold;
    public BombTowerTemplate bombTemplate;


    public GameObject bombRound;

    public GameObject arrowPrefab;
    public GameObject spawnPos;
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    public float spawnInterval = 1.5f; // 발사 속도?

    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    private bool canSpawn = false;
    private float timeSinceLastSpawn = 0.7f;

    public int BombLevel = 0;

    private bool checkCoin = false;

    public bool isClick = false;
    private bool BToggle;

    public int Startcoin = 210;


    public Vector2 BEnemyPos;
    public bool BombShot;


    private void Start()
    {

        bombRound.SetActive(false);

        playerGold = FindObjectOfType<GameManager>();
        // Testcoin 값만큼 PlayerGold를 처음 한 번만 감소시킵니다.
        if (!checkCoin)
        {
            //Debug.Log("d");
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
            }
        }

        enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position) || !enemy.activeSelf);

        canSpawn = enemyList.Count > 0;

        float bombround = bombTemplate.Bweapon[BombLevel].round;
        bombRound.transform.localScale = new Vector3(bombround, bombround, bombround);

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

        }

        if (BombTowerUI.buttonDown == true)
        {
            isClick = false;
            bombRound.SetActive(false);
            BombTowerUI.buttonDown = false;
        }

        StartCoroutine(DelayTm());

        if (enemyList.Count > 0) //FlowingObject != null && 
        {
            Vector2 targetPosition = enemyList[0].transform.position;
            BEnemyPos = targetPosition;
            BombShot = true;
        }
        else
        {
            BombShot = false;
            BEnemyPos = Vector2.zero;
        }


        if (isClick == true && BToggle == true)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isClick = false;
                    bombRound.SetActive(false);
                    BToggle = false;
                }
            }
                
        }
    }

    private IEnumerator DelayTm()
    {
        yield return new WaitForSeconds(0.2f);
        BToggle = true;
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
                    Debug.Log("타워 업그레이드 : 레벨 " + BombLevel);
                }
                else
                {
                    Debug.Log("돈이 없어");
                }
            }
            else
            {
                Debug.Log("최대 업그레이드 상태입니다");
            }
        }
        else
        {
            Debug.Log("Templat이 없어");
        }
    }

    private void OnMouseDown()
    {
        if (!isClick && BToggle == true)
        {
            isClick = true;
            bombRound.SetActive(true);
            BToggle = false;
        }
        
    }

    private void SpawnArrow()
    {
        var temp = Instantiate(arrowPrefab, spawnPos.transform.position, Quaternion.identity).GetComponent<Bomb>();

        temp.bombTower = GetComponent<BombTower>();
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
