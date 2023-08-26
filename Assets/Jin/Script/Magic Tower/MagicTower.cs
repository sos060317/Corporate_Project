using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPosition; // 프리팹을 소환할 위치 (Transform 타입)

    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    public float spawnInterval = 1.5f;

    private List<GameObject> enemyList = new List<GameObject>();
    private float timeSinceLastSpawn = 0f;

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

        enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position));

        if (enemyList.Count > 0)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                timeSinceLastSpawn = 0f;
                SpawnPrefabTowardsEnemy();
            }
        }
    }

    private bool IsWithinRadius(Vector2 position)
    {
        return Vector2.Distance(transform.position, position) <= detectionRadius;
    }

    private void SpawnPrefabTowardsEnemy()
    {
        if (enemyList.Count > 0)
        {
            GameObject targetEnemy = enemyList[0];
            Vector3 spawnPos = spawnPosition.position; // spawnPosition의 위치를 Vector3로 가져옴
            Vector2 direction = ((Vector2)targetEnemy.transform.position - (Vector2)spawnPos).normalized;

            GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            spawnedPrefab.transform.up = direction;

            enemyList.RemoveAt(0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
