using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn; // Prefab을 넣을 수 있는 List
    public Transform parentTransform; // 부모로 설정할 Transform

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        // 처음에 Prefab의 개수만큼 소환
        for (int i = 0; i < prefabsToSpawn.Count; i++)
        {
            SpawnPrefab(prefabsToSpawn[i]);
        }
    }

    private void Update()
    {
        // 각 Prefab이 없어졌을 때, 3초 후에 다시 생성
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            if (spawnedObjects[i] == null)
            {
                StartCoroutine(SpawnPrefabDelayed(prefabsToSpawn[i], 3f));
                spawnedObjects.RemoveAt(i); 
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

        spawnedObjects.Add(spawnedObject);
    }

    private IEnumerator SpawnPrefabDelayed(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnPrefab(prefab);
    }
}
