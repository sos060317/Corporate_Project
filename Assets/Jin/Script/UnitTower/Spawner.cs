using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TextMeshProUGUI UpgradeCostText;
    public TextMeshProUGUI DestroyCost;

    public TowerTemplate towerTemplate;

    //private GameManager playerGold;
    private int level = 0;
    private bool isSpawning = false;

    private List<GameObject> spawnedUnits = new List<GameObject>(); // 소환된 단위(Unit)를 저장할 리스트

    public List<GameObject> prefabsToSpawn; // 소환 가능한 Prefab을 넣을 수 있는 List
    public Transform parentTransform; // 부모로 설정할 Transform
    public GameObject Round;

    private bool SpawncostCheck = false;

    public int StartingCoin;


    private void Start()
    {
        //playerGold = FindObjectOfType<GameManager>();

        // 시작할 때 MaxUnit 수만큼 소환
        int initialUnitsToSpawn = towerTemplate.weapon[level].MaxUnit;
        SpawnUnits(initialUnitsToSpawn);

        // Testcoin 값만큼 PlayerGold를 처음 한 번만 감소시킴
        if (!SpawncostCheck)
        {
            GameManager.Instance.UseGold(towerTemplate.weapon[level].cost);
            SpawncostCheck = true;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            return;
        }

        UpdateCostText();
        DestroyCostText();

        // 활성화 상태가 아닌 단위(Unit)를 리스트에서 제거
        spawnedUnits.RemoveAll(unit => unit == null || !unit.activeSelf);

        if (!isSpawning && spawnedUnits.Count < towerTemplate.weapon[level].MaxUnit)
        {
            StartCoroutine(SpawnUnitsWithDelay(towerTemplate.weapon[level].SpawnTime));
        }

        float radius = towerTemplate.weapon[level].radius;
        Round.transform.localScale = new Vector3(radius, radius, radius);
    }

    private IEnumerator SpawnUnitsWithDelay(float delay)
    {
        isSpawning = true;

        // delay 시간만큼 대기
        yield return new WaitForSeconds(delay);

        int unitsToSpawn = towerTemplate.weapon[level].MaxUnit - spawnedUnits.Count;

        for (int i = 0; i < unitsToSpawn; i++)
        {
            // 소환 가능한 Prefab이 아직 남아있는지 확인
            if (prefabsToSpawn.Count > 0)
            {
                GameObject prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];
                SpawnPrefab(prefabToSpawn);
            }
            else
            {
                // 더 이상 소환할 수 있는 Prefab이 없으면 종료
                break;
            }
        }

        isSpawning = false;
    }

    private void SpawnUnits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // 소환 가능한 Prefab이 아직 남아있는지 확인
            if (prefabsToSpawn.Count > 0)
            {
                GameObject prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];
                SpawnPrefab(prefabToSpawn);
            }
            else
            {
                // 더 이상 소환할 수 있는 Prefab이 없으면 종료
                break;
            }
        }
    }

    public void DestroyTower()
    {
        Destroy(gameObject);
        GameManager.Instance.GetGold(towerTemplate.weapon[level].ResellCost);
    }

    private void UpdateCostText()
    {
        // costText가 null이 아니라면 TMP 텍스트 업데이트
        if (UpgradeCostText != null)
        {
            UpgradeCostText.text = towerTemplate.weapon[level + 1].cost.ToString();
        }
    }

    private void DestroyCostText()
    {
        // costText가 null이 아니라면 TMP 텍스트 업데이트
        if (DestroyCost != null)
        {
            DestroyCost.text = towerTemplate.weapon[level].ResellCost.ToString();
        }
    }

    private void SpawnPrefab(GameObject prefab)
    {
        GameObject spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);

        if (parentTransform != null)
        {
            spawnedObject.transform.SetParent(parentTransform);
        }

        spawnedUnits.Add(spawnedObject);
    }

    // 단위(Unit)가 사라질 때 호출할 메서드
    public void UnitDestroyed(GameObject unit)
    {
        if (spawnedUnits.Contains(unit))
        {
            spawnedUnits.Remove(unit);
        }
    }

    // 업그레이드 메서드
    public void UpgradeTower()
    {
        if (level < towerTemplate.weapon.Length - 1)
        {
            if (GameManager.Instance.currentGold >= towerTemplate.weapon[level + 1].cost)
            {
                float spcost = towerTemplate.weapon[level + 1].cost;
                GameManager.Instance.UseGold(spcost);

                level++;
                Debug.Log("타워 업그레이드: 레벨 " + level);
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
}
