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
    
    private NextWaveInfo nextWaveInfoObj;

    private List<string> enemyNameList;
    private List<int> enemyCountList;

    private void Start()
    {
        // Load Component
        path = GetComponent<PathCreator>();
        
        // Variable initialize
        spawnPos = path.path[0];
        curWaveIndex = -1;
        enemyNameList = new List<string>();
        enemyCountList = new List<int>();
        nextWaveInfoObj = transform.GetChild(0).GetComponent<NextWaveInfo>();

        WaveManager.Instance.waveStartEvent += NextWaveEvent;
        WaveManager.Instance.waveEndEvent += ShowNextWaveInfo;
        WaveManager.Instance.enemySpawnerCount++;

        ShowNextWaveInfo();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void NextWaveEvent()
    {
        curWaveIndex++;
        
        nextWaveInfoObj.gameObject.SetActive(false);

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

        WaveEnd();
    }
    
    private void ShowNextWaveInfo()
    {
        if (curWaveIndex >= wave.waves.Length)
        {
            nextWaveInfoObj.gameObject.SetActive(false);
            return;
        }
        
        if ((curWaveIndex + 1) >= wave.waves.Length)
        {
            return;
        }

        if (wave.waves[curWaveIndex + 1].waveDatas.Length == 0)
        {
            nextWaveInfoObj.gameObject.SetActive(false);
            return;
        }
        
        CalculateEnemyCount();
        
        nextWaveInfoObj.InitInfo(enemyNameList, enemyCountList);
        
        nextWaveInfoObj.gameObject.SetActive(true);
    }
    
    private void CalculateEnemyCount()
    {
        enemyNameList.Clear();
        enemyCountList.Clear();

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
    
    // 웨이브가 끝났을때 실행되는 함수
    private void WaveEnd()
    {
        WaveManager.Instance.WaveComplete();
    }
}
