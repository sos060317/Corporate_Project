using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadCreator))]
[RequireComponent(typeof(PathCreator))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveDetailsSO wave;

    private int curWaveIndex = -1;
    
    private PathCreator path;

    private Vector2 spawnPos;

    private WaitForSeconds enemySpawnTimer;

    public List<string> enemyNameList;
    public List<int> enemyCountList;

    private void Start()
    {
        // Load Component
        path = GetComponent<PathCreator>();
        
        // Variable initialize
        spawnPos = path.path[0];
        curWaveIndex = -1;

        WaveManager.Instance.waveEvent += NextWaveEvent;
        WaveManager.Instance.enemySpawnerCount++;

        CalculateEnemyCount();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void NextWaveEvent()
    {
        curWaveIndex++;
        
        CalculateEnemyCount();

        if (curWaveIndex >= wave.waves.Length)
        {
            return;
        }

        StartCoroutine(WaveRoutine());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator WaveRoutine()
    {
        foreach (var item in wave.waves[curWaveIndex].waveDatas)
        {
            enemySpawnTimer = new WaitForSeconds(item.enemySpawnInterval);

            for (int enemySpawnCount = 0;
                 enemySpawnCount < item.enemySpawnCount;
                 enemySpawnCount++)
            {
                var enemy = Instantiate(item.enemyType.enemyPrefab, spawnPos, Quaternion.identity);
                
                enemy.GetComponent<EnemyBase>().InitEnemy(path.path.CalculateEvenlySpacedPoints(0.1f), item.enemyType);

                yield return enemySpawnTimer;
            }

            yield return new WaitForSeconds(item.nextEnemyDelay);
        }
        
        WaveManager.Instance.WaveComplete();
    }
    
    private void CalculateEnemyCount()
    {
        enemyNameList.Clear();
        enemyCountList.Clear();
        
        if ((curWaveIndex + 1) >= wave.waves.Length)
        {
            return;
        }

        foreach (var item in wave.waves[curWaveIndex + 1].waveDatas)
        {
            if(enemyNameList.Contains(item.enemyType.enemyName))
            {
                continue;
            }
            else
            {
                enemyNameList.Add(item.enemyType.enemyName);
            }
        }

        for (int i = 0; i < enemyNameList.Count; i++)
        {
            enemyCountList.Add(0);
        }

        foreach (var item in wave.waves[curWaveIndex + 1].waveDatas)
        {
            enemyCountList[enemyNameList.FindIndex(x => x.Equals(item.enemyType.enemyName))] += item.enemySpawnCount;
        }
    }
}
