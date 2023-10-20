using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public TowerTemplate towerTemplate;

    //private PlayerGold playerGold;
    //private int level = 0;
    //private int currentUnitCount = 0; // 현재 소환된 유닛 수

    //public List<GameObject> prefabsToSpawn; // Prefab을 넣을 수 있는 List
    //public Transform parentTransform; // 부모로 설정할 Transform
    //public float SpawnTime = 5f;

    //private List<GameObject> spawnedObjects = new List<GameObject>();

    //public GameObject Round;


    //private void Start()
    //{
    //    playerGold = FindObjectOfType<PlayerGold>();
    //    SpawnInitialUnits();
    //}

    //private void SpawnInitialUnits()
    //{
    //    currentUnitCount = 0;
    //    SpawnUnits(towerTemplate.weapon[level].MaxUnit);
    //}

    //private void Update()
    //{
    //    if (currentUnitCount < towerTemplate.weapon[level].MaxUnit)
    //    {
    //        int unitsToSpawn = towerTemplate.weapon[level].MaxUnit - currentUnitCount;
    //        SpawnUnits(unitsToSpawn);
    //    }

    //    float radius = towerTemplate.weapon[level].radius;
    //    Round.transform.localScale = new Vector3(radius, radius, radius);
    //}

    //private void SpawnUnits(int count)
    //{
    //    for (int i = 0; i < count; i++)
    //    {
    //        SpawnPrefab(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)]);
    //    }
    //}

    //private void SpawnPrefab(GameObject prefab)
    //{
    //    GameObject spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);

    //    if (parentTransform != null)
    //    {
    //        spawnedObject.transform.SetParent(parentTransform);
    //    }

    //    spawnedObjects.Add(spawnedObject);
    //    currentUnitCount++;
    //}

    //// 업그레이드 메서드
    //public void UpgradeTower()
    //{
    //    if (level < towerTemplate.weapon.Length - 1)
    //    {
    //        if (playerGold.CurrentGold >= towerTemplate.weapon[level + 1].cost)
    //        {
    //            playerGold.CurrentGold -= towerTemplate.weapon[level + 1].cost;
    //            level++;
    //            Debug.Log("타워 업그레이드: 레벨 " + level);
    //        }
    //        else
    //        {
    //            Debug.Log("돈 부족하다");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("최대 업그레이드 상태 입니다.");
    //    }
    //}

    // 범위 키우기 까지, 유닛 재소환 오류가 남

    public TowerTemplate towerTemplate;

    private GameManager playerGold;
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
        playerGold = FindObjectOfType<GameManager>();

        // 시작할 때 MaxUnit 수만큼 소환
        int initialUnitsToSpawn = towerTemplate.weapon[level].MaxUnit;
        SpawnUnits(initialUnitsToSpawn);

        // Testcoin 값만큼 PlayerGold를 처음 한 번만 감소시킴
        if (!SpawncostCheck)
        {
            Debug.Log("d");
            playerGold.UseGold(towerTemplate.weapon[level].cost);
            SpawncostCheck = true;
        }
    }

    private void Update()
    {
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
            if (playerGold.currentGold >= towerTemplate.weapon[level + 1].cost)
            {
                float spcost = towerTemplate.weapon[level + 1].cost;
                playerGold.UseGold(spcost);

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
