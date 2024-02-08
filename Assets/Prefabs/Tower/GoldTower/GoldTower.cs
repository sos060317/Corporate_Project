using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class GoldTower : MonoBehaviour
{
    //public TextMeshProUGUI goldTowerUI;
    public GoldWindow goldTowerUI;

    public bool isClick;
    public bool UIClick;
    public GameObject goldBananaPrefab;
    public Vector2 spawnRange = new Vector2(3.5f, 1.5f);
    public float spawnRangeY = 1f;

    public GoldLevel goldLevel;
    
    [SerializeField]
    private bool isSpawn;

    [SerializeField]
    //private float spawnTime = 1f;
    private float timer = 0f;
    private int level = 0;
    //private bool delayCheck = false;
    //private bool firstClickCheck = true;
    bool firstClick = true;

    [SerializeField]
    private List<GameObject> spawnedBananas = new List<GameObject>();

    private void Start()
    {
        isSpawn = true;
        UIClick = true;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            if (firstClick)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.CompareTag("GoldTower") && !EventSystem.current.IsPointerOverGameObject())
                    {
                        UIClick = false;
                        goldTowerUI.buttonDown = false;
                        firstClick = false;
                    }
                }
            }
            else if (!firstClick && !EventSystem.current.IsPointerOverGameObject())
            {
                UIClick = true; // uiclick이 true가 되고 즉 uiclick이 false면 숨겨질때
                goldTowerUI.buttonDown = false;
                firstClick = true;
            }
        }

        if (Input.GetMouseButtonDown(1)) // Harvest
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("GoldTower") && !EventSystem.current.IsPointerOverGameObject())
                {
                    StartCoroutine(SetIsClickTrueForDuration(.5f));

                    foreach (GameObject banana in spawnedBananas)
                    {
                        if (banana != null)
                        {
                            GoldBanana goldBananaScript = banana.GetComponent<GoldBanana>();
                            if (goldBananaScript != null)
                            {
                                goldBananaScript.GetGold();
                            }
                        }
                    }

                    break;
                }
            }
        }

        if (isSpawn)
        {
            SpawnBanana();
        }
        CheckDestroyedBananas();

        if(goldTowerUI.buttonDown == true) // ui가 숨겨져 있으면
        {
           UIClick = true; // uiclick이 true가 되고 즉 uiclick이 false면 숨겨질때
           goldTowerUI.buttonDown = false;

        }

        if (Input.GetKeyDown(KeyCode.W)) // 나중에 래벨업후 삭제
        {
            LevelUp();
        }
    }

    public void UpgradeTower()
    {
        if (level < goldLevel.goldTowerLevel.Length - 1)
        {
            if (GameManager.Instance.currentGold >= goldLevel.goldTowerLevel[level + 1].Cost)
            {
                float cost = goldLevel.goldTowerLevel[level + 1].Cost;
                GameManager.Instance.UseGold(cost);

                level++;
                Debug.Log("타워 업그레이드: 레벨 " + level);
            }
            else
            {
                Debug.Log("돈 없");
            }
        }
        else
        {
            Debug.Log("풀업글");
        }
    }

    public void DestroyTower()
    {
        Destroy(gameObject);
        GameManager.Instance.GetGold(goldLevel.goldTowerLevel[level].ResellGold);
    }

    private void SpawnBanana()
    {
        timer += Time.deltaTime;

        if (timer >= goldLevel.goldTowerLevel[level].spawnTime)
        {
            float randomX = Random.Range(transform.position.x - spawnRange.x / 2f, transform.position.x + spawnRange.x / 2f);

            float randomY = Random.Range(transform.position.y - spawnRange.y / 2f, transform.position.y + spawnRange.y / 2f);

            randomX = Mathf.Clamp(randomX, transform.position.x - spawnRange.x / 2f, transform.position.x + spawnRange.x / 2f);
            randomY = Mathf.Clamp(randomY, transform.position.y - spawnRange.y / 2f, transform.position.y + spawnRange.y / 2f);

            randomY += spawnRangeY / 2;

            Vector2 randomPosition = new Vector2(randomX, randomY);

            GameObject newBanana = Instantiate(goldBananaPrefab, randomPosition, Quaternion.identity);
            spawnedBananas.Add(newBanana);


            GoldBanana goldBananaScript = newBanana.GetComponent<GoldBanana>();
            if (goldBananaScript != null)
            {
                goldBananaScript.InitializeLevel(level, goldLevel);
            }

            timer = 0f;
        }
    }

    private void LevelUp()
    {
        float how = 0;

        if(level < 3)
        {
            level = level + 1;
            how += 1;
        }

        Debug.Log(level);

    }

    private void CheckDestroyedBananas()
    {
        spawnedBananas.RemoveAll(banana => banana == null || banana.GetComponent<GoldBanana>() == null);
    }

    private System.Collections.IEnumerator SetIsClickTrueForDuration(float duration)
    {
        isClick = true;
        yield return new WaitForSeconds(duration);
        isClick = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        float offsetY = spawnRangeY / 2f;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z), new Vector3(spawnRange.x, spawnRange.y, 0f));
    }
}
