using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class BombTower : MonoBehaviour
{
    public TextMeshProUGUI UpgradeCostText;
    public TextMeshProUGUI DestroyCost;


    public BombWindow BombTowerUI;
    public Animator BombTowerAnim;

    //private GameManager playerGold;
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
    private float timeSinceLastSpawn = 1.9f;

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

        //playerGold = FindObjectOfType<GameManager>();
        // Testcoin 값만큼 PlayerGold를 처음 한 번만 감소시킵니다.
        if (!checkCoin)
        {
            //Debug.Log("d");
            GameManager.Instance.UseGold(bombTemplate.Bweapon[BombLevel].Bcost);
            checkCoin = true;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            BombTowerAnim.StartPlayback();
            return;
        }
        else
        {
            BombTowerAnim.StopPlayback();
        }

        UpdateCostText();
        DestroyCostText();

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
                //SpawnArrow();

                StartCoroutine(AttackAnimation());
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

    public void DestroyTower()
    {
        GameManager.Instance.GetGold(bombTemplate.Bweapon[BombLevel].ResellCost);
        Destroy(gameObject);
    }

    private void UpdateCostText()
    {
        if(BombLevel <= 2)
        {
            if (UpgradeCostText != null)
            {
                UpgradeCostText.text = bombTemplate.Bweapon[BombLevel + 1].Bcost.ToString();
            }
        }
        else
        {
            if (UpgradeCostText != null)
            {
                UpgradeCostText.text = ("Max!");
            }
        }
        
    }

    private void DestroyCostText()
    {
        // costText가 null이 아니라면 TMP 텍스트 업데이트
        if (DestroyCost != null)
        {
            DestroyCost.text = bombTemplate.Bweapon[BombLevel].ResellCost.ToString();
        }
    }

    private IEnumerator DelayTm()
    {
        yield return new WaitForSeconds(0.2f);
        BToggle = true;
    }

    private IEnumerator AttackAnimation()
    {
        BombTowerAnim.SetBool("ItShot", true);
        yield return new WaitForSeconds(0.1f);
        BombTowerAnim.SetBool("ItShot", false);
    }

    public void UpgradeTower()
    {
        if(bombTemplate != null)
        {
            if(BombLevel < bombTemplate.Bweapon.Length - 1)
            {
                if(GameManager.Instance.currentGold >= bombTemplate.Bweapon[BombLevel + 1].Bcost)
                {
                    float cost = bombTemplate.Bweapon[BombLevel + 1].Bcost;
                    GameManager.Instance.UseGold(cost);
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
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
