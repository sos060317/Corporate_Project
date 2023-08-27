using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_j : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject spawnPos;
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    public GameObject FlowingObject;
    public float followSpeed = 5.0f;
    public float spawnInterval = 1.5f;

    [SerializeField]
    public List<GameObject> enemyList = new List<GameObject>();
    private bool canSpawn = false;
    private float timeSinceLastSpawn = 0f;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag) && !enemyList.Contains(collider.gameObject))
            {
                enemyList.Add(collider.gameObject);
                // 새로운 오브젝트가 리스트에 추가되면 FlowingObject를 활성화
                if (FlowingObject != null)
                {
                    FlowingObject.SetActive(true);
                }
            }
        }

        enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position));

        canSpawn = enemyList.Count > 0;

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
            // 리스트가 비어 있으면 FlowingObject를 비활성화
            if (FlowingObject != null)
            {
                FlowingObject.SetActive(false);
            }
        }

        if (FlowingObject != null && enemyList.Count > 0)
        {
            Vector2 targetPosition = enemyList[0].transform.position;
            FlowingObject.transform.position = Vector2.MoveTowards(FlowingObject.transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private void SpawnArrow()
    {
        Instantiate(arrowPrefab, spawnPos.transform.position, Quaternion.identity);
    }

    private bool IsWithinRadius(Vector2 position)
    {
        return Vector2.Distance(transform.position, position) <= detectionRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green; // 그린 스펠링 까먹어서 당황할뻔 슈벌
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
