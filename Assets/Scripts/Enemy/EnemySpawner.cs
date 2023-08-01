using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadCreator))]
[RequireComponent(typeof(PathCreator))]
public class EnemySpawner : MonoBehaviour
{
    #region Header

    [Space(10)]
    [Header("랜덤으로 스폰할 EnemyPrefab")]

    #endregion
    #region Tooltip

    [Tooltip("배열에 넣은 EnemyPrefab 중 하나를 랜덤으로 스폰한다.")]

    #endregion
    [SerializeField] private GameObject[] enemyPrefab;

    #region Header

    [Space(10)]
    [Header("적 소환 시간")]

    #endregion
    [SerializeField] private float spawnTime;
    
    private PathCreator path;

    private float spawnTimer;

    private Vector2 spawnPos;

    private void Start()
    {
        // Load Component
        path = GetComponent<PathCreator>();
        
        // Variable initialize
        spawnPos = path.path[0];

        spawnTimer = spawnTime;
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (spawnTimer <= 0)
        {
            // Enemy Spawn Logic
            
            spawnTimer = spawnTime;
        }

        spawnTimer -= Time.deltaTime;
    }
}
