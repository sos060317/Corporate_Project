using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //[SerializeField]
    //private TowerTemplate towerTemplate;

    //public int SpawnCount = 3;
    //public List<GameObject> prefabsToSpawn; // Prefab을 넣을 수 있는 List
    //public Transform parentTransform; // 부모로 설정할 Transform
    //public float SpawnTime = 5f;

    //private List<GameObject> spawnedObjects = new List<GameObject>();
    //private int level = 0;


    //private void Start()
    //{
    //    SpawnCount = towerTemplate.weapon[level].MaxUnit;

    //    // 처음에 Prefab의 개수만큼 소환
    //    for (int i = 0; i < SpawnCount; i++)
    //    {
    //        //SpawnPrefab(prefabsToSpawn[i]);
    //        SpawnPrefab(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)]);
    //    }
    //}

    //private void Update()
    //{
    //    for (int i = 0; i < spawnedObjects.Count; i++)
    //    {
    //        if (spawnedObjects[i].activeSelf == false)
    //        {
    //            StartCoroutine(SpawnPrefabDelayed(prefabsToSpawn[i], SpawnTime));
    //            spawnedObjects.RemoveAt(i);
    //            break;
    //        }
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
    //}

    //private IEnumerator SpawnPrefabDelayed(GameObject prefab, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SpawnPrefab(prefab);
    //}

    public TowerTemplate towerTemplate;

    private PlayerGold playerGold;
    private int level = 0;
    private int currentUnitCount = 0; // 현재 소환된 유닛 수

    public List<GameObject> prefabsToSpawn; // Prefab을 넣을 수 있는 List
    public Transform parentTransform; // 부모로 설정할 Transform
    public float SpawnTime = 5f;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    public GameObject Round;


    private void Start()
    {
        playerGold = FindObjectOfType<PlayerGold>();
        SpawnInitialUnits();
    }

    private void SpawnInitialUnits()
    {
        currentUnitCount = 0;
        SpawnUnits(towerTemplate.weapon[level].MaxUnit);
    }

    private void Update()
    {
        if (currentUnitCount < towerTemplate.weapon[level].MaxUnit)
        {
            int unitsToSpawn = towerTemplate.weapon[level].MaxUnit - currentUnitCount;
            SpawnUnits(unitsToSpawn);
        }

        float radius = towerTemplate.weapon[level].radius;
        Round.transform.localScale = new Vector3(radius, radius, radius);
    }

    private void SpawnUnits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnPrefab(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)]);
        }
    }

    private void SpawnPrefab(GameObject prefab)
    {
        GameObject spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);

        if (parentTransform != null)
        {
            spawnedObject.transform.SetParent(parentTransform);
        }

        spawnedObjects.Add(spawnedObject);
        currentUnitCount++;
    }

    // 업그레이드 메서드
    public void UpgradeTower()
    {
        if (level < towerTemplate.weapon.Length - 1)
        {
            if (playerGold.CurrentGold >= towerTemplate.weapon[level + 1].cost)
            {
                playerGold.CurrentGold -= towerTemplate.weapon[level + 1].cost;
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
