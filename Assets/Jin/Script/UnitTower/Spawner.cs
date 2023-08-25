using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn; // Prefab�� ���� �� �ִ� List
    public Transform parentTransform; // �θ�� ������ Transform

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        // ó���� Prefab�� ������ŭ ��ȯ
        for (int i = 0; i < prefabsToSpawn.Count; i++)
        {
            SpawnPrefab(prefabsToSpawn[i]);
        }
    }

    private void Update()
    {
        // �� Prefab�� �������� ��, 3�� �Ŀ� �ٽ� ����
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
